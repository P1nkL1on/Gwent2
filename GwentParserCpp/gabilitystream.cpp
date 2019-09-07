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


GAbilityStream::operator =(const GAbilityStream &abilityStream)
{
    m_words = abilityStream.m_words;
    m_pos = abilityStream.m_pos;
    m_text = abilityStream.m_text;
}

QStringList GAbilityStream::separateText(const QString &text) const
{
    QString textSpaced = text;
    foreach (const QString op, m_operators)
        textSpaced = textSpaced.replace(QString(op), m_separator + QString(op) + m_separator);
    return textSpaced.split(m_separator, QString::SplitBehavior::SkipEmptyParts);
}
