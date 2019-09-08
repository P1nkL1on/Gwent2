#include "gunit.h"

GParse *GUnit::createNew() const
{
    return new GUnit();
}

GParseRes GUnit::parseFrom(GAbilityStream &stream)
{
    const auto errMessage = awaits(
                stream,
                QStringList()
                << "rarity"
                << "tag"
                << "host"
                << "target"
                << "place",
                QList<GParse*>()
                << (m_colors = new GColorCondition())
                << (m_tags = new GTagCondition())
                << (m_host = new GHost())
                << (m_target = new GTarget())
                << (m_places = new GPlaceCondition()));

    if (!errMessage.isEmpty())
        return QString("invalid unit(s), because %1")
                .arg(errMessage.message());
    return QString();
}

QString GUnit::toString() const
{
    const QString none = "", space = " ";
    const QString colorString = m_colors->toString();
    const QString tagString   = m_tags->toString();
    const QString hostString  = m_host->toString();
    const QString unitString  = m_target->toString();
    const QString placeString = m_places->toString();
    return QString("%1%2%3%4%5%6%7%8%9")
            .arg(colorString).arg(colorString.isEmpty()? none : space)
            .arg(tagString).arg(tagString.isEmpty()? none : space)
            .arg(hostString).arg(hostString.isEmpty()? none : space)
            .arg(unitString).arg(placeString.isEmpty()? none : space)
            .arg(placeString);
}
