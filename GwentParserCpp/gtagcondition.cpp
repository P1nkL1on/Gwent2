#include "gtagcondition.h"

#include "gtag.h"
#include "gnegate.h"

GParse *GTagCondition::createNew() const
{
    return new GTagCondition();
}

GErr GTagCondition::parseFrom(GAbilityStream &stream)
{
    return GParse::awaitsAnyCountOf(
                stream,
                m_orSeparators,
                QList<GParse*>()
                    << static_cast<GParse*>((new GNegate<GTag>()))
                    << static_cast<GParse*>(new GTag()),
                QStringList() << "tag negation" << "tag",
                m_tags);
}

QString GTagCondition::toString() const
{
    QStringList tags;
    foreach (GParse* tag, m_tags)
        tags << tag->toString();
    return toStringSeparators(tags, m_orSeparators);
}

int GTagCondition::count() const
{
    return m_tags.length();
}
