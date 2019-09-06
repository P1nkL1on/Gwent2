#ifndef GPARSE_H
#define GPARSE_H

#include "gparseres.h"
#include <QList>
#include "gabilitystream.h"

class GParse
{
public:
    GParse() = default;
    virtual GParse* createNew() const = 0;
    virtual GParseRes parseFrom(GAbilityStream &stream) = 0;
    virtual QString toString() const = 0;
protected:
    GParseRes parseEnum(
            GAbilityStream& stream,
            const QStringList &variants,
            const QString &purpose,
            int &index) const;
    QString toStringEnum(
            const QStringList &variants,
            const QString &purpose,
            const int index) const;
    static GParseRes awaits(
            GAbilityStream &stream,
            const QList<GParse*> &pretendents,
            GParse *&winner);
    static GParseRes awaitsAnyCountOf(
            GAbilityStream &stream,
            const QStringList separators,
            const QList<GParse*> &pretendents,
            QList<GParse*> &winners);
    static GParseRes awaitAnyFlags(
            GAbilityStream &stream,
            const QList<GParse*> &flags,
            QList<GParse*> &winners);
};

#endif // GPARSE_H

