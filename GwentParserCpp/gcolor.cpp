#include "gcolor.h"


GParse *GColor::copy() const
{
    GColor* cop = new GColor();
    cop->m_rarity = m_rarity;
    return cop;
}

QString GColor::parseFrom(GAbilityStream &stream)
{
    const QString color = stream.nextWord();
    for (int i = 0; i < m_rarities.length(); ++i)
        if (color == m_rarities[i]){
            m_rarity = Rarity(i);
            return QString();
        }
    return QString("'%1' does not represent a rarity!").arg(color);
}

QString GColor::toString() const
{
    if (m_rarity < 0 || m_rarity >= m_rarities.length())
        return QString("?rarity?");
    return m_rarities[m_rarity];
}
