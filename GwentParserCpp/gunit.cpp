#include "gunit.h"

GParse *GUnit::createNew() const
{
    return new GUnit();
}

GErr GUnit::parseFrom(GAbilityStream &stream)
{
    GAbilityStream unitStream(stream);
    const auto err = awaits(
                unitStream,
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
    if (!err.isEmpty())
        return GErr(unitStream.pos(), QString("can't parse targeting unit")) += err;
    if (m_target->isMultipleUnits())
        return GErr(stream.pos(), QString("awaited targeting of 1 unit!"));
    if (m_places->count() >= 2)
        return GErr(stream.pos(), QString("1 unit can't be in several different places!"));
    stream = unitStream;
    return GErr();
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
