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
                QStringList() << m_separator,
                QList<GParse*>()
                    << static_cast<GParse*>(new GTag())
                    << static_cast<GParse*>((new GNegate<GTag>())),
                m_tags);
}

QString GTagCondition::toString() const
{
    QString res;
    foreach (GParse* tag, m_tags)
        res += QString("%1%2")
                .arg(res.isEmpty()? "" : QString(" %1 ").arg(m_separator))
                .arg(tag->toString());
    return res;
}
