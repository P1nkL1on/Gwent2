#include "gtagcondition.h"

#include "gtag.h"
#include "gnegate.h"

GParse *GTagCondition::createNew() const
{
    return new GTagCondition();
}

GParseRes GTagCondition::parseFrom(GAbilityStream &stream)
{
    return GParse::awaitsAnyCountOf(
                stream,
                m_separators,
                QList<GParse*>()
                    << static_cast<GParse*>((new GNegate<GTag>()))
                    << static_cast<GParse*>(new GTag()),
                m_tags);
}

QString GTagCondition::toString() const
{
    QStringList tags;
    foreach (GParse* tag, m_tags)
        tags << tag->toString();
    return toStringSeparators(tags, m_separators);
}
