#ifndef GCOLOR_H
#define GCOLOR_H

#include "gnegatable.h"

class GColor : public GNegatable
{
public:
    GColor() = default;
    virtual GParse* copy() const override;
    virtual QString parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    enum Rarity {bronze = 0, silver = 1, gold = 2};
    Rarity m_rarity;

    const QStringList m_rarities = QStringList() << "bronze" << "silver" << "gold";
};

#endif // GCOLOR_H
