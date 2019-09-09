#ifndef GPLACECONDITION_H
#define GPLACECONDITION_H

#include "gcondition.h"
#include "gplace.h"

class GPlaceCondition : public GParse, public GCondition
{
public:
    GPlaceCondition(const QString &pretext = "in");
    virtual GParse* createNew() const override;
    virtual GErr parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
    virtual int count() const override;
protected:
    QList<GParse*> m_places;
    QString m_awaitedPretext;
};


#endif // GPLACECONDITION_H
