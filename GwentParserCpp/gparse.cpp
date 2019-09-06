#include "gparse.h"

QString GParse::awaits(GAbilityStream &stream, const QList<GParse*> &pretendents, GParse *&winner)
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

QString GParse::awaitsAnyCountOf(GAbilityStream &stream, const QString separator, const QList<GParse *> &pretendents, QList<GParse *> &winners)
{
    QString errorMessage;
    do{
        GParse* winner = nullptr;
        QList<GParse *> newPretendents;
        foreach (GParse* pretendent, pretendents)
            newPretendents << pretendent->copy();

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

        if (separator.isEmpty())
            continue;
        const QString sep = stream.nextWord();
        if (sep != separator)
            return QString("'%1' is not a separator '%2'!")
                    .arg(sep).arg(separator);
    }while(!stream.end());
}
