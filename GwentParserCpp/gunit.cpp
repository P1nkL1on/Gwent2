#include "gunit.h"

GParse *GUnit::createNew() const
{
    return new GUnit();
}

GParseRes GUnit::parseFrom(GAbilityStream &stream)
{
    const QString errStart = "can't find targeting description, because ";
    QList<GParse*> flags;
    GParseRes errorMessage = GParse::awaitAnyFlags(
                stream,
                QList<GParse*>()
                    << static_cast<GParse*>(new GColorCondition())
                    << static_cast<GParse*>(new GTagCondition())
                    << static_cast<GParse*>(new GHost()),
                flags);
//    if (!errorMessage.isEmpty())
//        return QString("%1%2").arg(errStart).arg(errorMessage.message());
    m_colors = static_cast<GColorCondition*>(flags[0]);
    m_tags   = static_cast<GTagCondition*>(flags[1]);
    m_host   = static_cast<GHost*>(flags[2]);
    if (m_host == nullptr) m_host = new GHost();

    m_target = new GTarget();
    const GParseRes errTargetMessage = m_target->parseFrom(stream);
    if (!errTargetMessage.isEmpty())
        return QString("%1%2%3")
                .arg(errStart)
                .arg(errTargetMessage.message())
                .arg(errorMessage.isEmpty()? "" : QString(" becasuse %1").arg(errorMessage.message()));

    m_places = new GPlaceCondition();
    const GParseRes errPlaceMessage = m_places->parseFrom(stream);
    if (errPlaceMessage.isFatal())
        return QString("%1%2").arg(errStart).arg(errPlaceMessage.message());
    if (!errPlaceMessage.isEmpty() && m_places != nullptr){
        delete m_places;
        m_places = nullptr;
    }
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
