#ifndef GUNIT_H
#define GUNIT_H

#include "gcolorcondition.h"
#include "gplace.h"
#include "ghost.h"

class GUnit : public GParse
{
public:
    GUnit() = default;
    virtual GParse* createNew() const override;
    virtual QString parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    GColorCondition *m_color = nullptr;
    GHost *m_host = nullptr;
    GPlace *m_place = nullptr;
};

#endif // GUNIT_H
