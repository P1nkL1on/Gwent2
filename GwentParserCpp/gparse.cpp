#include "gparse.h"

QString GParse::awaits(
        GAbilityStream &stream,
        const QList<GParse*> &pretendents,
        GParse *&winner)
{
    QString awaitErrorMessage;
    foreach (GParse *pretendent, pretendents){
        GAbilityStream pretendentStream(stream);
        const QString errMessage = pretendent->parseFrom(pretendentStream);
        if (errMessage.isEmpty()){
            stream = pretendentStream;
            winner = pretendent;
            foreach (GParse* p, pretendents)
                if (p != winner)
                    delete p;
            return QString();
        }
        awaitErrorMessage += QString("%1%2")
                .arg(awaitErrorMessage.isEmpty()? "" : " and ")
                .arg(errMessage);
    }
    foreach (GParse* p, pretendents)
        delete p;
    return awaitErrorMessage;
}

QString GParse::awaitsAnyCountOf(
        GAbilityStream &stream,
        const QStringList separators,
        const QList<GParse *> &pretendents,
        QList<GParse *> &winners)
{
    QString errorMessage;
    do{
        GParse* winner = nullptr;
        QList<GParse *> newPretendents;
        foreach (GParse* pretendent, pretendents)
            newPretendents << pretendent->createNew();

        errorMessage = awaits(stream, newPretendents, winner);
        if (!errorMessage.isEmpty()){
            if (winners.isEmpty())
                return errorMessage;
            return QString();
        }
        winners << winner;
        if (stream.end()){
            if (winners.isEmpty())
                return QString("empty list readen!");
            return QString();
        }

        if (separators.isEmpty())
            continue;
        const QString sep = stream.nextWord();
        if (!separators.contains(sep))
            return QString("'%1' is not a separator")
                    .arg(sep);
    }while(!stream.end());
}

QString GParse::awaitAnyFlags(GAbilityStream &stream, const QList<GParse *> &flags, QList<GParse *> &winners)
{
    QString errorMessage;
}

QString GParse::parseEnum(GAbilityStream &stream, const QStringList &variants, const QString &purpose, int &index) const
{
    const QString value = stream.nextWord();
    index = variants.indexOf(value);
    if (index < 0)
        return QString("'%1' does not represent a %2!").arg(value).arg(purpose);
    return QString();
}

QString GParse::toStringEnum(const QStringList &variants, const QString &purpose, const int index) const
{
    if (index < 0 || index >= variants.length())
        return QString("?%1?").arg(purpose);
    return variants[index];
}
