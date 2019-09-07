#include "gparse.h"

GParseRes GParse::awaits(
        GAbilityStream &stream,
        const QList<GParse*> &pretendents,
        GParse *&winner)
{
    QString awaitErrorMessage;
    foreach (GParse *pretendent, pretendents){
        GAbilityStream pretendentStream(stream);
        const GParseRes errMessage = pretendent->parseFrom(pretendentStream);
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
                .arg(errMessage.message());
    }
    foreach (GParse* p, pretendents)
        delete p;
    return awaitErrorMessage;
}

GParseRes GParse::awaitsOptional(GAbilityStream &stream, GParse *pretendent, GParse *defaultClass)
{
    GAbilityStream pretendentStream(stream);
    const GParseRes errMessage = pretendent->parseFrom(pretendentStream);
    if (errMessage.isEmpty()){
        stream = pretendentStream;
        return QString();
    }
    delete pretendent;
    if (defaultClass != nullptr)
        pretendent = defaultClass->createNew();
    return errMessage;
}

GParseRes GParse::awaits(GAbilityStream &stream, const QString &word)
{
    GAbilityStream findInStream(stream);
    if (stream.end())
        return QString("text ends!");
    if (findInStream.nextWord() != word)
        return QString("can't find '%1'!").arg(word);
    stream = findInStream;
    return QString();
}

GParseRes GParse::awaits(GAbilityStream &stream, const QStringList &wordSequence)
{
    return QString();
}

GParseRes GParse::awaitsAnyCountOf(
        GAbilityStream &stream,
        const QStringList separators,
        const QList<GParse *> &pretendents,
        QList<GParse *> &winners,
        bool canBeEmpty)
{
    do{
        GParse* winner = nullptr;
        QList<GParse *> newPretendents;
        foreach (GParse* pretendent, pretendents)
            newPretendents << pretendent->createNew();

        const GParseRes errorMessage = awaits(stream, newPretendents, winner);
        if (!errorMessage.isEmpty()){
            // parse zero elemts
            if (!winners.length())
                return canBeEmpty? QString() : errorMessage;
            return GParseRes(
                        QString("can't parse sequence, because separator isn't followed by element, because %1")
                        .arg(errorMessage.message()));
        }
        winners << winner;
        if (stream.end()){
            if (winners.isEmpty() && !canBeEmpty)
                return QString("empty list readen!");
            return QString();
        }

        if (separators.isEmpty())
            continue;

        GAbilityStream findSepStream(stream);
        const QString sep = findSepStream.nextWord();
        if (!separators.contains(sep))
            return QString();
        stream = findSepStream;
    }while(!stream.end());
    return QString("can't parse sequence, because element doesn't follow separator!");
}

//GParseRes GParse::awaitAnyFlags(GAbilityStream &stream, const QList<GParse *> &flags, QList<GParse *> &winners)
//{
//    int nonNullFlags = 0;
//    QString errMessages = "can't parse optional sequence, because ";
//    foreach (GParse* flag, flags){
//        const GParseRes errorMessage = flag->parseFrom(stream);
//        if (errorMessage.isFatal())
//            return QString("%2 %1").arg(errorMessage.message()).arg(errMessages);
//        if (!errorMessage.isEmpty()){
//            winners << nullptr;
//            errMessages += (flags.first() == flag? "" : " and ") + errorMessage.message().mid(errorMessage.message().lastIndexOf("because"));
//            delete flag;
//            continue;
//        }
//        winners << flag;
//        ++nonNullFlags;
//    }
//    if (!nonNullFlags)
//        return errMessages;
//    return QString();
//}

GParseRes GParse::parseEnum(GAbilityStream &stream, const QStringList &variants, const QString &purpose, int &index) const
{
    GAbilityStream streamWord = stream;
    const QString value = streamWord.nextWord();
    index = variants.indexOf(value);
    if (index < 0)
        return QString("'%1' does not represent a %2!").arg(value).arg(purpose);
    stream = streamWord;
    return QString();
}

QString GParse::toStringEnum(const QStringList &variants, const QString &purpose, const int index) const
{
    if (index < 0 || index >= variants.length())
        return QString("?%1?").arg(purpose);
    return variants[index];
}
