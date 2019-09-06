#include "gplacecondition.h"

#include "gplace.h"

GParse *GPlaceCondition::createNew() const
{
    return new GPlaceCondition();
}

QString GPlaceCondition::parseFrom(GAbilityStream &stream)
{
    if (stream.nextWord() != "in")
        return QString("'in' for specifying place awaited!");
    return GParse::awaitsAnyCountOf(
                stream,
                m_separators,
                QList<GParse*>() << static_cast<GParse*>(new GPlace()),
                m_places);
}

QString GPlaceCondition::toString() const
{
    QString res;
    foreach (GParse* place, m_places)
        res += QString("%1%2")
                .arg(res.isEmpty()? "" : QString(" %1 ")
                .arg(place == m_places.last()? m_separators.last() : m_separators.first()))
                .arg(place->toString());
    return res;
}
