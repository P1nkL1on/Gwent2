#ifndef GTABCONDITION_H
#define GTABCONDITION_H

#include "gtag.h"

class GTagCondition : public GParse
{
public:
    GTagCondition() = default;
    virtual GParse* createNew() const override;
    virtual QString parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    QList<GParse*> m_tags;
    const QString m_separator = "or";
};


#endif // GTABCONDITION_H
