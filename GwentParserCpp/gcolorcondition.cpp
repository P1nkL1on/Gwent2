#include "gcolorcondition.h"

#include "gcolor.h"
#include "gnegate.h"

GParse *GColorCondition::createNew() const
{
    return new GColorCondition();
}

GParseRes GColorCondition::parseFrom(GAbilityStream &stream)
{
    return GParse::awaitsAnyCountOf(
                stream,
                m_separators,
                QList<GParse*>()
                    << static_cast<GParse*>((new GNegate<GColor>()))
                    << static_cast<GParse*>(new GColor()),
                m_colors);
}

QString GColorCondition::toString() const
{
    QStringList colors;
    foreach (GParse* color, m_colors)
        colors << color->toString();
    return toStringSeparators(colors, m_separators);
}
