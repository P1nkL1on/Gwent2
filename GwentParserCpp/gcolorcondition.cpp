#include "gcolorcondition.h"

#include "gcolor.h"
#include "gnegate.h"

GParse *GColorCondition::createNew() const
{
    return new GColorCondition();
}

QString GColorCondition::parseFrom(GAbilityStream &stream)
{
    return GParse::awaitsAnyCountOf(
                stream,
                QStringList() << m_separator,
                QList<GParse*>()
                    << static_cast<GParse*>(new GColor())
                    << static_cast<GParse*>((new GNegate<GColor>())),
                m_colors);
}

QString GColorCondition::toString() const
{
    QString res;
    foreach (GParse* color, m_colors)
        res += QString("%1%2")
                .arg(res.isEmpty()? "" : QString(" %1 ").arg(m_separator))
                .arg(color->toString());
    return res;
}
