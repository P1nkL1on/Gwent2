#include "gplacecondition.h"

#include "gplace.h"

GParse *GPlaceCondition::createNew() const
{
    return new GPlaceCondition();
}

GParseRes GPlaceCondition::parseFrom(GAbilityStream &stream)
{
    GParseRes errMessage = awaits(stream, "in");
    if (!errMessage.isEmpty())
        return QString();
    return GParse::awaitsAnyCountOf(
                stream,
                m_separators,
                QList<GParse*>() << static_cast<GParse*>(new GPlace()),
                m_places);
}

QString GPlaceCondition::toString() const
{
    QStringList places;
    foreach (GParse* place, m_places)
        places << place->toString();
    return toStringSeparators(places, m_separators);
}
