#include "gabilitystream.h"


GAbilityStream::GAbilityStream(const QString &abilityText):
    m_text(abilityText.toLower())
{
    m_words = new QStringList();
    *m_words = separateText(m_text);
    m_pos = 0;
}

GAbilityStream::GAbilityStream(const GAbilityStream &abilityStream)
{
    m_words = abilityStream.m_words;
    m_pos = abilityStream.m_pos;
    m_text = abilityStream.m_text;
}

bool GAbilityStream::isNull() const
{
    return m_words == nullptr || m_words->isEmpty();
}

bool GAbilityStream::end() const
{
    return m_pos >= m_words->length();
}

QString GAbilityStream::word() const
{
    return m_words->at(m_pos);
}

QString GAbilityStream::peekNextWord() const
{
    return m_words->at(m_pos + 1);
}

QString GAbilityStream::nextWord()
{
    return m_words->at(m_pos++);
}

QString GAbilityStream::wordsAround(const int position) const
{
    return wordsAround(position, position);
}

QString GAbilityStream::wordsAround(const int positionFrom, const int positionTo, const int amplitude) const
{
    const int prevIndex = positionFrom < amplitude? 0 : (positionFrom - amplitude);
    const int nextIndex = positionTo + amplitude >= m_words->length()? (m_words->length() - 1) : (positionTo + amplitude);

    QString mess = "\n";
    int startInd = -1, endInd = -1, ind = 0;
    for (int i = prevIndex; i <= nextIndex; ++i){
        const QString addition = (i == prevIndex? "" : " ") + QString("%1").arg(m_words->at(i));
        mess += addition;
        if (positionFrom == i)
            startInd = ind + (i == prevIndex? 0 : 1);
        if ((nextIndex < positionTo? nextIndex : positionTo) == i)
            endInd = ind + addition.length();
        ind += addition.length();
    }
    mess += '\n' + QString().leftJustified(startInd, ' ') + QString().leftJustified(endInd - startInd, '~');
    return mess;
}


GAbilityStream::operator =(const GAbilityStream &abilityStream)
{
    m_words = abilityStream.m_words;
    m_pos = abilityStream.m_pos;
    m_text = abilityStream.m_text;
}

int GAbilityStream::pos() const
{
    return m_pos;
}

QStringList GAbilityStream::separateText(const QString &text) const
{
    QString textSpaced = text;
    foreach (const QString op, m_operators)
        textSpaced = textSpaced.replace(QString(op), m_separator + QString(op) + m_separator);
    return textSpaced.split(m_separator, QString::SplitBehavior::SkipEmptyParts);
}
