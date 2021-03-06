#ifndef  __AITree__
#define  __AITree__

#include "AITreeData.h"
#include <unordered_map>
#include <string>

class  GameObject;
class AIBase;
class  XEntity;

class AITree
{
public:
	AITree(void);
	~AITree(void);
	void Initial(XEntity* e);
	void EnableBehaviourTree(bool enable);
	bool SetBehaviourTree(const char* name);
	void SetVariable(const char* name, float value);
	void SetVariable(const char* name, int value);
	void SetVariable(const char* name, uint value);
	void SetVariable(const char* name, bool value);
	void SetVariable(const char* name, GameObject* value);
	bool ResetVariable(const char* name);
	float GetFloatVariable(const char* name);
	int GetIntVariable(const char* name);
	uint GetUintVariable(const char* name);
	bool GetBoolVariable(const char* name);
	GameObject* GetGoVariable(const char* name);
	void TickBehaviorTree(XEntity* entity);


private:
	const char* *composites;
	AITreeData* _tree_data;
	AIBase* _tree_behaviour;
	XEntity* _entity;
	bool _enable;
};


#endif