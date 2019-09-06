#include "gplacecondition.h"

#include "gplace.h"

GParse *GPlaceCondition::createNew() const
{
    return new GPlaceCondition();
}

GParseRes GPlaceCondition::parseFrom(GAbilityStream &stream)
{
    if (stream.end())
        return QString("can't find place description!");
    GAbilityStream findInStream(stream);
    if (findInStream.nextWord() != "in")
        return QString("can't find 'in' before place description!");
    stream = findInStream;
    return GParse::awaitsAnyCountOf(
                stream,
                m_separators,
                QList<GParse*>() << static_cast<GParse*>(new GPlace()),
                m_places);
}

QString GPlaceCondition::toString() const
{
    QString res = "in ";
    foreach (GParse* place, m_places)
        res += QString("%1%2")
                .arg(place == m_places.first()? "" : QString("%1 ")
                .arg(place == m_places.last()?(" " + m_separators.last() ): m_separators.first()))
                .arg(place->toString());
    return res;
}
