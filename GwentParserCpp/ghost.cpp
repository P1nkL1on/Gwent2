#include "ghost.h"

GParse *GHost::createNew() const
{
    return new GHost();
}

QString GHost::parseFrom(GAbilityStream &stream)
{
    int index;
    const QString errorMessage = parseEnum(stream, m_hosts, "host", index);
    if (errorMessage.isEmpty())
        m_host = Host(index);
    return errorMessage;
}

QString GHost::toString() const
{
    return toStringEnum(m_hosts, "host", m_host);
}
