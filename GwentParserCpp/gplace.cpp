#include "gplace.h"

GParse *GPlace::copy() const
{
    GPlace* cop = new GPlace();
    cop->m_place = m_place;
    return cop;
}

QString GPlace::parseFrom(GAbilityStream &stream)
{
    const QString place = stream.nextWord();
    for (int i = 0; i < m_places.length(); ++i)
        if (place == m_places[i]){
            m_place = Place(i);
            return QString();
        }
    return QString("'%1' does not represent a palce!").arg(place);
}

QString GPlace::toString() const
{
    if (m_place < 0 || m_place >= m_places.length())
        return QString("?place?");
    return m_places[m_place];
}
