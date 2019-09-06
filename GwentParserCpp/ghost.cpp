#include "ghost.h"

GParse *GHost::copy() const
{
    GHost* cop = new GHost();
    cop->m_host = m_host;
    return cop;
}

QString GHost::parseFrom(GAbilityStream &stream)
{
    const QString host = stream.nextWord();
    for (int i = 0; i < m_hosts.length(); ++i)
        if (host == m_hosts[i]){
            m_host = Host(i);
            return QString();
        }
    return QString("'%1' does not represent a host!").arg(host);
}

QString GHost::toString() const
{
    if (m_host < 0 || m_host >= m_hosts.length())
        return QString("?host?");
    return m_hosts[m_host];
}
