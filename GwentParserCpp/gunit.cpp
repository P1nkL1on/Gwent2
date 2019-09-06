#include "gunit.h"

GParse *GUnit::createNew() const
{
    return new GUnit();
}

GParseRes GUnit::parseFrom(GAbilityStream &stream)
{
    QList<GParse*> flags;
    GParseRes errorMessage = GParse::awaitAnyFlags(
                stream,
                QList<GParse*>()
                    << static_cast<GParse*>(new GColorCondition())
                    << static_cast<GParse*>(new GTagCondition())
                    << static_cast<GParse*>(new GHost())
                    << static_cast<GParse*>(new GTarget())
                    << static_cast<GParse*>(new GPlaceCondition()),
                flags);
    if (!errorMessage.isEmpty())
        return QString("can't find targeting description, because %1").arg(errorMessage.message());
    m_colors = static_cast<GColorCondition*>(flags[0]);
    m_tags   = static_cast<GTagCondition*>(flags[1]);
    m_host   = static_cast<GHost*>(flags[2]);
    m_target = static_cast<GTarget*>(flags[3]);
    m_places = static_cast<GPlaceCondition*>(flags[4]);

    if (m_host == nullptr)
        m_host = new GHost();
    if (m_target == nullptr)
        return QString("can't find targeting description, because no target given!");
    return QString();
}

QString GUnit::toString() const
{
    const QString none = "", space = " ";
    const QString colorString = m_colors == nullptr? none : m_colors->toString();
    const QString tagString   = m_tags == nullptr? none : m_tags->toString();
    const QString hostString  = m_host->toString();
    const QString unitString  = m_target->toString();
    const QString placeString = m_places == nullptr? none : m_places->toString();
    return QString("%1%2%3%4%5%6%7%8%9")
            .arg(colorString).arg(colorString.isEmpty()? none : space)
            .arg(tagString).arg(tagString.isEmpty()? none : space)
            .arg(hostString).arg(hostString.isEmpty()? none : space)
            .arg(unitString).arg(placeString.isEmpty()? none : space)
            .arg(placeString);
}
