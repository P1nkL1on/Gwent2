#ifndef GCOLORCONDITION_H
#define GCOLORCONDITION_H

#include "gcondition.h"
#include "gcolor.h"

class GColorCondition : public GParse, public GCondition
{
public:
    GColorCondition() = default;
    virtual GParse* createNew() const override;
    virtual GErr parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
    virtual int count() const override;
protected:
    QList<GParse*> m_colors;
};

#endif // GCOLORCONDITION_H
