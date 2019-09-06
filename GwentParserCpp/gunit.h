#ifndef GUNIT_H
#define GUNIT_H

#include "gcolorcondition.h"
#include "gplacecondition.h"
#include "gtagcondition.h"
#include "ghost.h"
#include "gtarget.h"

class GUnit : public GParse
{
public:
    GUnit() = default;
    virtual GParse* createNew() const override;
    virtual GParseRes parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    GColorCondition *m_colors = nullptr;
    GTagCondition *m_tags = nullptr;
    GHost *m_host = nullptr;
    GTarget *m_target = nullptr;
    GPlaceCondition *m_places = nullptr;
};

#endif // GUNIT_H
