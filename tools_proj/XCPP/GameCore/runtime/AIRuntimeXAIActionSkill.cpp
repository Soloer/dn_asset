/*
* <auto-generated>
*	 The code is generated by tools
*	 Edit manually this code will lead to incorrect behavior
* </auto-generated>
*/

#include "AIRuntimeXAIActionSkill.h"


AIRuntimeXAIActionSkill::~AIRuntimeXAIActionSkill()
{
	delete GameObjectmAIArgTarget;
}

void AIRuntimeXAIActionSkill::Init(AITaskData* data)
{
	AIBase::Init(data);
	StringmAIArgSkillScript = data->vars["StringmAIArgSkillScript"]->val.get<std::string>(); 
	
}


AIStatus AIRuntimeXAIActionSkill::OnTick(XEntity* entity)
{
	GameObjectmAIArgTarget = _tree->GetGoVariable("target");
	
	return AITreeImpleted::XAIActionSkillUpdate(entity,StringmAIArgSkillScript,GameObjectmAIArgTarget);
}