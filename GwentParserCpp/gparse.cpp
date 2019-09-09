#include "gparse.h"

GErr GParse::awaits(
        GAbilityStream &stream,
        const QList<GParse*> &pretendents,
        const QStringList &purposes,
        GParse *&winner)
{
    const QString purposesFailString = "awaits %1, but failed";
    QString ps = "";
    foreach (const QString &p, purposes)
        ps += p + (p == purposes.last()? "" : " or ");
    GErr awaitError(stream.pos(), purposesFailString.arg(ps));

    foreach (GParse *pretendent, pretendents){
        GAbilityStream pretendentStream(stream);
        const GErr err = pretendent->parseFrom(pretendentStream);
        if (err.isEmpty()){
            stream = pretendentStream;
            winner = pretendent;
            foreach (GParse* p, pretendents)
                if (p != winner)
                    delete p;
            return GErr();
        }
        awaitError += err;
    }
    foreach (GParse* p, pretendents)
        delete p;
    return awaitError;
}
GErr GParse::awaits(GAbilityStream &stream, GParse *&pretendent)
{
    GAbilityStream pretendentStream(stream);
    const GErr errMessage = pretendent->parseFrom(pretendentStream);
    if (!errMessage.isEmpty()){
        GParse* defaultPretendent = pretendent->createNew();
        delete pretendent;
        pretendent = defaultPretendent;
    }else
        stream = pretendentStream;
    return errMessage;
}

GErr GParse::awaits(GAbilityStream &stream, const QString &word)
{
    return awaits(stream, QStringList() << word);
}

GErr GParse::awaits(GAbilityStream &stream, const QStringList &wordSequence)
{
    GAbilityStream findInStream(stream);
    QString awaitedString = "";
    QString givenString = "";
    foreach (const QString &string, wordSequence)
        awaitedString += string + (awaitedString.isEmpty()? "" : " ");
    for (int i = 0; i < wordSequence.length(); ++i){
        if (findInStream.end())
            return GErr(findInStream.pos(), QString("awaited '%1', but given %2!")
                    .arg(awaitedString).arg(givenString.isEmpty()?
                    "nothing" : QString("'%1'").arg(givenString)));
        const QString awaits = wordSequence[i];
        const QString given = findInStream.nextWord();
        givenString += (i == 0? "" : " ") + given;
        if (awaits != given)
            return GErr(findInStream.pos(), QString("awaited '%1', but given '%2'!")
                    .arg(awaitedString).arg(givenString));
    }
    stream = findInStream;
    return GErr();
}

GErr GParse::awaits(GAbilityStream &stream, const QStringList &elementPurpose, QList<GParse *> &elementSequence)
{
    Q_ASSERT(elementPurpose.length() == elementSequence.length());

    for (int i = 0; i < elementPurpose.length(); ++i){
        const auto err = awaits(stream, elementSequence[i]);
        if (!err.isEmpty())
            return GErr(stream.pos(), QString("invalid %1").arg(elementPurpose[i])) += err;
    }
    return GErr();
}

GErr GParse::awaitsAnyCountOf(
        GAbilityStream &stream,
        const QStringList &separators,
        const QList<GParse *> &pretendents,
        const QStringList &pretendentPurposes,
        QList<GParse *> &winners,
        bool canBeEmpty)
{
    QStringList separatorUsed;
    do{
        GParse* winner = nullptr;
        QList<GParse *> newPretendents;
        foreach (GParse* pretendent, pretendents)
            newPretendents << pretendent->createNew();

        const GErr err = awaits(stream, newPretendents, pretendentPurposes, winner);
        if (!err.isEmpty()){
            // parse zero elemts
            if (!winners.length())
                break;//return canBeEmpty? QString() : errorMessage;
            return GErr(stream.pos(), QString("can't parse sequence, because separator isn't followed by element")) += err;
        }
        winners << winner;
        if (stream.end()){
            if (winners.isEmpty() && !canBeEmpty)
                return GErr(stream.pos(), "empty list readen!");
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
        return GErr(stream.pos(), "can't find sequence!");
    if (!separatorUsed.isEmpty() && !separatorUsed.contains(separators.last()))
        return GErr(stream.pos(), "ambegious separators using!");
    return GErr();
}

GErr GParse::parseEnum(GAbilityStream &stream, const QStringList &variants, const QString &purpose, int &index) const
{
    GAbilityStream streamWord = stream;
    const QString value = streamWord.nextWord();
    index = variants.indexOf(value);
    if (index < 0 && variants.first().isEmpty()){
        index = 0;
        return GErr();
    }
    if (index < 0)
        return GErr(stream.pos(), QString("'%1' does not represent a %2!").arg(value).arg(purpose));
    stream = streamWord;
    return GErr();
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
