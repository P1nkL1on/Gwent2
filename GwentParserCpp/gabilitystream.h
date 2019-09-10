#ifndef GABILITYSTREAM_H
#define GABILITYSTREAM_H

#include <QStringList>

class GAbilityStream
{
public:
    GAbilityStream() = default;
    GAbilityStream(const QString &abilityText);
    GAbilityStream(const GAbilityStream &abilityStream);
    bool isNull() const;
    bool end() const;
    int pos() const;
    QString word() const;
    QString peekNextWord() const;
    QString nextWord();
    QString wordsAround(const int position) const;
    QString wordsAround(const int positionFrom, const int positionTo, const int amplitude = 2) const;

    operator =(const GAbilityStream &abilityStream);

protected:
    int m_pos = -1;
    QString m_text = QString();
    QStringList *m_words = nullptr;

    const QStringList m_operators =  QStringList() << "," << "." << ":" << "non-";
    const QChar m_separator = ' ';
    QStringList separateText(const QString &text) const;
};

#endif // GABILITYSTREAM_H
