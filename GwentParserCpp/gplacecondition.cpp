#include "gplacecondition.h"

#include "gplace.h"

GPlaceCondition::GPlaceCondition(const QString &pretext):
    m_awaitedPretext(pretext)
{
}

GParse *GPlaceCondition::createNew() const
{
    return new GPlaceCondition();
}

GErr GPlaceCondition::parseFrom(GAbilityStream &stream)
{
    GErr errMessage = awaits(stream, m_awaitedPretext);
    if (!errMessage.isEmpty())
        return GErr();
    return GParse::awaitsAnyCountOf(
                stream,
                m_andSeparators,
                QList<GParse*>() << static_cast<GParse*>(new GPlace()),
                QStringList() << "place name",
                m_places);
}

QString GPlaceCondition::toString() const
{
    QStringList places;
    foreach (GParse* place, m_places)
        places << place->toString();
    return toStringSeparators(places, m_andSeparators);
}

int GPlaceCondition::count() const
{
    return m_places.length();
}
