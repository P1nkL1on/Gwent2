#ifndef GTABCONDITION_H
#define GTABCONDITION_H

#include "gtag.h"

class GTagCondition : public GParse
{
public:
    GTagCondition() = default;
    virtual GParse* createNew() const override;
    virtual GParseRes parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    QList<GParse*> m_tags;
    const QStringList m_separators = QStringList() << "," << "or";
};


#endif // GTABCONDITION_H
