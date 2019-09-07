#include "gunit.h"

GParse *GUnit::createNew() const
{
    return new GUnit();
}

GParseRes GUnit::parseFrom(GAbilityStream &stream)
{
    const QString errStart = "can't find unit description, because";
    GParseRes errMessage("");

    errMessage = awaitsOptional(stream, m_colors = new GColorCondition());
    if (!errMessage.isEmpty())
        return QString("%1 invalid rarity, because %2").arg(errStart).arg(errMessage.message());

    errMessage = awaitsOptional(stream, m_tags = new GTagCondition());
    if (!errMessage.isEmpty())
        return QString("%1 invalid tags, because %2").arg(errStart).arg(errMessage.message());

    errMessage = awaitsOptional(stream, m_host = new GHost(), new GHost());

    m_target = new GTarget();
    errMessage = m_target->parseFrom(stream);
    if (!errMessage.isEmpty())
        return QString("%1 invalid target, because %2").arg(errStart).arg(errMessage.message());

    errMessage = awaitsOptional(stream, m_places = new GPlaceCondition());
    if (!errMessage.isEmpty())
        return QString("%1 invalid place, because %2").arg(errStart).arg(errMessage.message());

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
