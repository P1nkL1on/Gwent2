#ifndef GPLACE_H
#define GPLACE_H


class GPlace : public GParse
{
public:
    GPlace() = default;
    virtual GParse* copy() const override;
    virtual QString parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    enum Place {battlefield = 0, hand = 1, deck = 2, graveyard = 3};
    Place m_place;

    const QStringList m_places = QStringList()
            << "battlefield" << "hand" << "deck" << "graveyard";
};


#endif // GPLACE_H
