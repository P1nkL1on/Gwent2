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

GParseRes GParse::awaits(GAbilityStream &stream, GParse *&pretendent)
{
    GAbilityStream pretendentStream(stream);
    const GParseRes errMessage = pretendent->parseFrom(pretendentStream);
    if (!errMessage.isEmpty()){
        GParse* defaultPretendent = pretendent->createNew();
        delete pretendent;
        pretendent = defaultPretendent;
    }else
        stream = pretendentStream;
    return errMessage;
}

GParseRes GParse::awaits(GAbilityStream &stream, const QString &word)
{
    return awaits(stream, QStringList() << word);
}

GParseRes GParse::awaits(GAbilityStream &stream, const QStringList &wordSequence)
{
    GAbilityStream findInStream(stream);
    QString awaitedString = "";
    QString givenString = "";
    foreach (const QString &string, wordSequence)
        awaitedString += string + (awaitedString.isEmpty()? "" : " ");
    for (int i = 0; i < wordSequence.length(); ++i){
        if (findInStream.end())
            return QString("awaited '%1', but given %2!")
                    .arg(awaitedString).arg(givenString.isEmpty()?
                                                "nothing" : QString("'%1'").arg(givenString));
        const QString awaits = wordSequence[i];
        const QString given = findInStream.nextWord();
        givenString += (i == 0? "" : " ") + given;
        if (awaits != given)
            return QString("awaited '%1', but given '%2'!")
                    .arg(awaitedString).arg(givenString);
    }
    stream = findInStream;
    return QString();
}

GParseRes GParse::awaits(GAbilityStream &stream, const QStringList &elementPurpose, QList<GParse *> &elementSequence)
{
    Q_ASSERT(elementPurpose.length() == elementSequence.length());

    for (int i = 0; i < elementPurpose.length(); ++i){
        const auto errMessage = awaits(stream, elementSequence[i]);
        if (!errMessage.isEmpty())
            return QString("invalid %1, because %2").arg(elementPurpose[i]).arg(errMessage.message());
    }
    return QString();
}

GParseRes GParse::awaitsAnyCountOf(
        GAbilityStream &stream,
        const QStringList separators,
        const QList<GParse *> &pretendents,
        QList<GParse *> &winners,
        bool canBeEmpty)
{
    QStringList separatorUsed;
    do{
        GParse* winner = nullptr;
        QList<GParse *> newPretendents;
        foreach (GParse* pretendent, pretendents)
            newPretendents << pretendent->createNew();

        const GParseRes errorMessage = awaits(stream, newPretendents, winner);
        if (!errorMessage.isEmpty()){
            // parse zero elemts
            if (!winners.length())
                break;//return canBeEmpty? QString() : errorMessage;
            return GParseRes(
                        QString("can't parse sequence, because separator isn't followed by element, because %1")
                        .arg(errorMessage.message()));
        }
        winners << winner;
        if (stream.end()){
            if (winners.isEmpty() && !canBeEmpty)
                return QString("empty list readen!");
            break;//return QString();
        }

        if (separators.isEmpty())
            continue;

        GAbilityStream findSepStream(stream);
        const QString sep = findSepStream.nextWord();
        if (!separators.contains(sep))
            break;//return QString();
        stream = findSepStream;
        separatorUsed << sep;
    }while(!stream.end());
    // return QString("can't parse sequence, because element doesn't follow separator!");
    if (!canBeEmpty && winners.isEmpty())
        return QString("can't find sequence!");
    if (!separatorUsed.isEmpty() && !separatorUsed.contains(separators.last()))
        return QString("ambegious separators using!");
    return QString();
}

GParseRes GParse::parseEnum(GAbilityStream &stream, const QStringList &variants, const QString &purpose, int &index) const
{
    GAbilityStream streamWord = stream;
    const QString value = streamWord.nextWord();
    index = variants.indexOf(value);
    if (index < 0 && variants.first().isEmpty()){
        index = 0;
        return QString();
    }
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

QString GParse::toStringSeparators(const QStringList &elements, const QStringList &separators) const
{
    QString res = "";
    foreach (const QString &element, elements)
        res += QString("%1%2")
                .arg(element == elements.first()? "" : QString("%1 ")
                .arg(element == elements.last()?(" " + separators.last() ): separators.first()))
                .arg(element);
    return res;
}
