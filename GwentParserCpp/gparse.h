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
    static GParseRes awaits(
            GAbilityStream &stream,
            const QList<GParse*> &pretendents,
            GParse *&winner);
    static GParseRes awaits(GAbilityStream &stream,
            GParse *&pretendent);
    static GParseRes awaits(
            GAbilityStream &stream,
            const QString &word);
    static GParseRes awaits(
            GAbilityStream &stream,
            const QStringList &wordSequence);
    static GParseRes awaits(GAbilityStream &stream,
            const QStringList &elementPurpose,
            QList<GParse *> &elementSequence);
    static GParseRes awaitsAnyCountOf(GAbilityStream &stream,
            const QStringList separators,
            const QList<GParse*> &pretendents,
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

