#ifndef GPARSE_H
#define GPARSE_H

#include <QString>
#include <QList>
#include "gabilitystream.h"

class GParse
{
public:
    GParse() = default;
    virtual GParse* createNew() const = 0;
    virtual QString parseFrom(GAbilityStream &stream) = 0;
    virtual QString toString() const = 0;
protected:
    QString parseEnum(
            GAbilityStream& stream,
            const QStringList &variants,
            const QString &purpose,
            int &index) const;
    QString toStringEnum(
            const QStringList &variants,
            const QString &purpose,
            const int index) const;
    static QString awaits(
            GAbilityStream &stream,
            const QList<GParse*> &pretendents,
            GParse *&winner);
    static QString awaitsAnyCountOf(
            GAbilityStream &stream,
            const QStringList separators,
            const QList<GParse*> &pretendents,
            QList<GParse*> &winners);
    static QString awaitAnyFlags(
            GAbilityStream &stream,
            const QList<GParse*> &flags,
            QList<GParse*> &winners);
};

#endif // GPARSE_H

