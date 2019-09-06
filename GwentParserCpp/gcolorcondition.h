#ifndef GCOLORCONDITION_H
#define GCOLORCONDITION_H

#include "gcolor.h"

class GColorCondition : public GParse
{
public:
    GColorCondition() = default;
    virtual GParse* createNew() const override;
    virtual GParseRes parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    QList<GParse*> m_colors;
    const QString m_separator = "or";
};

#endif // GCOLORCONDITION_H
