#ifndef GPARSE_H
#define GPARSE_H

#include <QString>
#include <QList>
#include "gabilitystream.h"

class GParse
{
public:
    GParse() = default;
    virtual GParse* copy() const = 0;
    virtual QString parseFrom(GAbilityStream &stream) = 0;
    virtual QString toString() const = 0;
protected:
    static QString awaits(
            GAbilityStream &stream,
            const QList<GParse*> &pretendents,
            GParse *&winner);
    static QString awaitsAnyCountOf(
            GAbilityStream &stream,
            const QString separator,
            const QList<GParse*> &pretendents,
            QList<GParse*> &winners);
    static QString awaitAnyFlags(
            GAbilityStream &stream,
            const QString separator,
            const QList<GParse*> &flags,
            QList<GParse*> &winners);
};

#endif // GPARSE_H

