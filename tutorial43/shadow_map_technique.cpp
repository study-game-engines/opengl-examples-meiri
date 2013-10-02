/*

	Copyright 2011 Etay Meiri

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

#include "shadow_map_technique.h"


static const char* pEffectFile = "shaders/shadow_map.glsl";

ShadowMapTechnique::ShadowMapTechnique(): Technique(pEffectFile)
{
}

bool ShadowMapTechnique::Init()
{
    if (!CompileProgram("ShadowMap")) {
        return false;
    }

    m_WVPLocation = GetUniformLocation("gWVP");
	m_WVLocation = GetUniformLocation("gWV");

    if (m_WVPLocation == INVALID_UNIFORM_LOCATION ||
		m_WVLocation == INVALID_UNIFORM_LOCATION) {
        return false;
    }

    return true;
}


void ShadowMapTechnique::SetWVP(const Matrix4f& WVP)
{
    glUniformMatrix4fv(m_WVPLocation, 1, GL_TRUE, (const GLfloat*)WVP.m);
}


void ShadowMapTechnique::SetWV(const Matrix4f& WV)
{
	glUniformMatrix4fv(m_WVLocation, 1, GL_TRUE, (const GLfloat*)WV.m);
}
