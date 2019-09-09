#ifndef GPARSE_H
#define GPARSE_H

#include "gparseerr.h"
#include <QList>
#include "gabilitystream.h"

class GParse
{
public:
    GParse() = default;
    virtual GParse* createNew() const = 0;
    virtual GErr parseFrom(GAbilityStream &stream) = 0;
    virtual QString toString() const = 0;
protected:
    GErr parseEnum(
            GAbilityStream& stream,
            const QStringList &variants,
            const QString &purpose,
            int &index) const;
    static GErr awaits(GAbilityStream &stream,
            const QList<GParse*> &pretendents, const QStringList &purposes,
            GParse *&winner);
    static GErr awaits(GAbilityStream &stream,
            GParse *&pretendent);
    static GErr awaits(
            GAbilityStream &stream,
            const QString &word);
    static GErr awaits(
            GAbilityStream &stream,
            const QStringList &wordSequence);
    static GErr awaits(GAbilityStream &stream,
            const QStringList &elementPurpose,
            QList<GParse *> &elementSequence);
    static GErr awaitsAnyCountOf(GAbilityStream &stream,
            const QStringList &separators,
            const QList<GParse*> &pretendents, const QStringList &pretendentPurposes,
            QList<GParse*> &winners,
            bool canBeEmpty = true);


    QString toStringEnum(
            const QStringList &variants,
            const QString &purpose,
            const int index) const;
    QString toStringSeparators(
            const QStringList &elements,
            const QStringList &separators) const;
};

#endif // GPARSE_H

