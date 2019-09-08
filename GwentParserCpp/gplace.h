#ifndef GPLACE_H
#define GPLACE_H

#include "gparse.h"

class GPlace : public GParse
{
public:
    GPlace() = default;
    virtual GParse* createNew() const override;
    virtual GParseRes parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    enum Place {battlefield = 0, hand = 1, deck = 2, graveyard = 3, startingDeck = 4, anyCard = 5};
    Place m_place = battlefield;

    const QStringList m_places = QStringList()
            << "battlefield" << "hand" << "deck" << "graveyard";
};


#endif // GPLACE_H
