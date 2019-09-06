#include "gplace.h"

GParse *GPlace::createNew() const
{
    return new GPlace();
}

GParseRes GPlace::parseFrom(GAbilityStream &stream)
{
    int index;
    const GParseRes errorMessage = parseEnum(stream, m_places, "place", index);
    if (errorMessage.isEmpty())
        m_place = Place(index);
    return errorMessage;
}

QString GPlace::toString() const
{
    return toStringEnum(m_places, "place", m_place);
}
