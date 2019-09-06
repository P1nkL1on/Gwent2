#include "gunit.h"

GParse *GUnit::copy() const
{
    GUnit *cop = new GUnit();
    cop->m_host = m_host;
    cop->m_place = m_place;
    cop->m_rarity = m_rarity;
}

QString GUnit::parseFrom(GAbilityStream &stream)
{

}

QString GUnit::toString() const
{

}
