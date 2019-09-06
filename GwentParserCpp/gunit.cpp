#include "gunit.h"

GParse *GUnit::createNew() const
{
    GUnit *cop = new GUnit();
    cop->m_host = m_host;
    cop->m_place = m_place;
    cop->m_color = m_color;
}

QString GUnit::parseFrom(GAbilityStream &stream)
{

}

QString GUnit::toString() const
{

}
