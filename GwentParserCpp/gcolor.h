#ifndef GCOLOR_H
#define GCOLOR_H

#include "gnegatable.h"

class GColor : public GNegatable
{
public:
    GColor() = default;
    virtual GParse* createNew() const override;
    virtual QString parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    enum Rarity {anycolor = 0, bronze = 1, silver = 2, gold = 3};
    Rarity m_rarity;

    const QStringList m_rarities = QStringList() << QString() << "bronze" << "silver" << "gold";
};

#endif // GCOLOR_H
