#ifndef GPLACECONDITION_H
#define GPLACECONDITION_H

#include "gplace.h"

class GPlaceCondition : public GParse
{
public:
    GPlaceCondition() = default;
    virtual GParse* createNew() const override;
    virtual GParseRes parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    QList<GParse*> m_places;
    const QStringList m_separators = QStringList() << "," << "and";
};


#endif // GPLACECONDITION_H
