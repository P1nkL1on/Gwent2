#include "gcolor.h"


GParse *GColor::createNew() const
{
    return new GColor();
}

GErr GColor::parseFrom(GAbilityStream &stream)
{
    int index;
    const GErr errorMessage = parseEnum(stream, m_rarities, "rarity", index);
    if (errorMessage.isEmpty())
        m_rarity = Rarity(index);
    return errorMessage;
}

QString GColor::toString() const
{
    return toStringEnum(m_rarities, "rarity", m_rarity);
}
