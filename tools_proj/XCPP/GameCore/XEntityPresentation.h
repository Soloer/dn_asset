/*
 * <auto-generated>
 *	 The code is generated by tools
 *	 Edit manually this code will lead to incorrect behavior
 * </auto-generated>
 */

#ifndef __XEntityPresentation__
#define __XEntityPresentation__

#include"NativeReader.h"
#include <vector>
#include"Log.h"

struct XEntityPresentationRow
{
	
	uint uid;
	char prefab[MaxStringSize];
	char name[MaxStringSize];
	char bonesuffix[MaxStringSize];
	char animlocation[MaxStringSize];
	char skilllocation[MaxStringSize];
	char curvelocation[MaxStringSize];
	float boundradius;
	float boundradiusoffset[MaxArraySize];
	float boundheight;
	char hugemonstercolliders[MaxStringSize];
	float scale;
	char uiavatarangle[MaxStringSize];
	float uiavatarscale;
	char avatarpos[MaxStringSize];
	bool huge;
	char entergame[MaxStringSize];
	int angluarspeed;
	char idle[MaxStringSize];
	char attackidle[MaxStringSize];
	char fishingidle[MaxStringSize];
	char walk[MaxStringSize];
	char attackwalk[MaxStringSize];
	char run[MaxStringSize];
	char attackrun[MaxStringSize];
	char runleft[MaxStringSize];
	char attackrunleft[MaxStringSize];
	char runright[MaxStringSize];
	char attackrunright[MaxStringSize];
	char movefx[MaxStringSize];
	char freeze[MaxStringSize];
	char freezefx[MaxStringSize];
	std::string hit_f[MaxArraySize];
	std::string hit_l[MaxArraySize];
	std::string hit_r[MaxArraySize];
	float hitbackoffsettimescale[MaxArraySize];
	std::string hitfly[MaxArraySize];
	float hitflyoffsettimescale[MaxArraySize];
	std::string hit_roll[MaxArraySize];
	float hitrolloffsettimescale[MaxArraySize];
	float hitback_recover[MaxArraySize];
	float hitfly_bounce_getup[MaxArraySize];
	float hitroll_recover;
	char hitfx[MaxStringSize];
	char death[MaxStringSize];
	char deathfx[MaxStringSize];
	std::string hitcurves[MaxArraySize];
	char deathcurve[MaxStringSize];
	char a[MaxStringSize];
	char aa[MaxStringSize];
	char aaa[MaxStringSize];
	char aaaa[MaxStringSize];
	char aaaaa[MaxStringSize];
	char otherskills[MaxStringSize];
	char skillnum[MaxStringSize];
	char dash[MaxStringSize];
	char ultra[MaxStringSize];
	char appear[MaxStringSize];
	char disappear[MaxStringSize];
	char fishingcast[MaxStringSize];
	char fishingpull[MaxStringSize];
	char fishingwait[MaxStringSize];
	char fishingwin[MaxStringSize];
	char fishinglose[MaxStringSize];
	char dance[MaxStringSize];
	char kiss[MaxStringSize];
	char inheritactionone[MaxStringSize];
	char inheritactiontwo[MaxStringSize];
	char atlas[MaxStringSize];
	char avatar[MaxStringSize];
	char atlas2[MaxStringSize];
	char avatar2[MaxStringSize];
	bool shadow;
	char feeble[MaxStringSize];
	char feeblefx[MaxStringSize];
	char armorbroken[MaxStringSize];
	char superarmorrecoveryskill[MaxStringSize];
	char recoveryfx[MaxStringSize];
	char recoveryhitslowfx[MaxStringSize];
	char recoveryhitstopfx[MaxStringSize];
};

class XEntityPresentation:public NativeReader
{
public:
	XEntityPresentation(void);
	void ReadTable();
	void GetRow(int val,XEntityPresentationRow* row);
	int GetLength();

protected:
	std::string name;
	std::vector<XEntityPresentationRow> m_data;
};


extern "C"
{
	ENGINE_INTERFACE_EXPORT int iGetXEntityPresentationLength();
	ENGINE_INTERFACE_EXPORT void iGetXEntityPresentationRow(int idx,XEntityPresentationRow* row);
};

#endif