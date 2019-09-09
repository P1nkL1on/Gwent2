#ifndef GTARGET_H
#define GTARGET_H

#include "gparse.h"

class GTarget : public GParse
{
public:
    GTarget() = default;
    virtual GParse* createNew() const override;
    virtual GErr parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
    bool isMultipleUnits() const;
protected:
    // units and unit equals to other units / other unit
    enum Target {unit = 0, units = 1, self = 2};
    Target m_target = unit;
    const QStringList m_targets = QStringList() << "unit" << "units" << "self";
};

#endif // GTARGET_H
