#ifndef GNEGATE_H
#define GNEGATE_H

#include "gnegatable.h"
#include "gparse.h"


template <typename T>
class GNegate : public GParse
{
public:
    GNegate() = default;
    virtual GParse* createNew() const override;
    virtual GParseRes parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
private:
    GNegatable* m_value = nullptr;
    const QString m_negateWord = "non-";
};

template <typename T>
GParse *GNegate<T>::createNew() const
{
    GNegate<T>* cop = new GNegate<T>();
    cop->m_value = m_value;
    return cop;
}

template<typename T>
GParseRes GNegate<T>::parseFrom(GAbilityStream &stream)
{
    const QString negateWord = stream.nextWord();
    if (negateWord != m_negateWord)
        return QString("'%1' is not a negation!").arg(negateWord);
    GParse* value;
    const GParseRes errMessage =
        GParse::awaits(stream, QList<GParse*>() << static_cast<GParse*>(new T()), value);
    m_value = static_cast<GNegatable*>(value);
    return errMessage;
}

template <typename T>
QString GNegate<T>::toString() const
{
    return QString("%1%2").arg(m_negateWord).arg(m_value->toString());
}


#endif // GNEGATE_H
