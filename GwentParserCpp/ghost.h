#ifndef GOWN_H
#define GOWN_H

#include "gparse.h"

class GHost : public GParse
{
public:
    GHost() = default;
    virtual GParse* createNew() const override;
    virtual GErr parseFrom(GAbilityStream &stream) override;
    virtual QString toString() const override;
protected:
    enum Host {any = 0, enemy = 1, ally = 2};
    Host m_host = any;

    const QStringList m_hosts = QStringList() << QString() << "enemy" << "ally";
};

#endif // GOWN_H
