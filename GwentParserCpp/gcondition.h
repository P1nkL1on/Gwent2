#ifndef GCONDITION_H
#define GCONDITION_H

#include <QStringList>

class GCondition
{
public:
    virtual int count() const;
protected:
    const QStringList m_orSeparators = QStringList() << "," << "or";
    const QStringList m_andSeparators = QStringList() << "," << "and";
};

#endif // GCONDITION_H
