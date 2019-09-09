#ifndef GTABCONDITION_H
#define GTABCONDITION_H

#include "gcondition.h"
#include "gtag.h"

class GTagCondition : public GParse, public GCondition
{
public:
    GTagCondition() = default;
    virtual GParse* createNew() const override;
    virtual GErr parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
    virtual int count() const override;
protected:
    QList<GParse*> m_tags;
};


#endif // GTABCONDITION_H
