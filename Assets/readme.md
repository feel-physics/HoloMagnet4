### �N���̂���

����Unity2017.2.2���g���Ă��܂�

1. �V�K�v���W�F�N�g�����AMRTK�i����2017.2.1.4�j�����܂�
3. Asset�t�H���_�Ƀ��|�W�g���̃t�H���_�iHoloMagnet36�j����荞�݂܂�
4. Scenes�t�H���_��Scene2D���J���܂�
5. Managers�I�u�W�F�N�g�̎q��Sharing�I�u�W�F�N�g��I�����܂�
6. Sharing Stage��Server Address��ݒ肵�܂�
7. ���s���܂�

### ������@

- ��̕��Ƀ{�^��������ł��܂��B
- ���я��̓I�u�W�F�N�g�̃{�^���̕��я��Ɠ����ŁA������c

1. Grobal Params�I�u�W�F�N�g�����i�܂��ŏ��ɍs���Ă��������j
2. ���΂�z���W��1.5�ɌŒ�E�����̃g�O��
3. ���ʏ�̕��ʎ��j�𐶐��E�폜�̃g�O��
4. 3�����̕��ʎ��j�𐶐��E�폜�̃g�O��
5. ���͐��̕\���E��\���̃g�O��
6. �f�o�b�O���O�̕\���E��\���̃g�O��

Grobal Param�̎����Ă���ϐ���Unity Player�Ŏ��s�����Ƃ���
Sharing�I�u�W�F�N�g�̃C���X�y�N�^�ɕ\������܂�

### Sharing�֌W�̎Q�l�ɂȂ肻���ȃX�N���v�g

- SyncSpawnedGlobalParams.cs
  - GlobalParams�I�u�W�F�N�g���`���܂�
  - GlobalParams�I�u�W�F�N�g�́A�ォ��Sharing�ɎQ�������A�v�����ɂ������I�ɐ�������܂�
  - Sharing�S�̂̕ϐ��i���ʏ�̕��ʎ��j�̐����E��\���Ȃǁj�������܂�
- CompassPlacer2DSpawner.cs
  - ���ʏ�̕��ʎ��j�𐶐��E�폜�̃g�O�����s���܂�
  - GlobalParams���ێ�����ShouldShowCompass2D�ϐ���true/false��؂�ւ��܂�
  - true�ɂ����Ƃ��́ACompassPlacer2D�I�u�W�F�N�g�𐶐����܂��B�ォ��Sharing�ɎQ�������A�v�����ɂ������I�ɐ�������܂��B
  - false�ɂ����Ƃ��́ACompassPlacer2D�I�u�W�F�N�g���폜���A���ʎ��j���폜���s���܂�
- GoAndBackMovement.cs
  - �_���΂��s�����藈���肳���܂�
  - GlobalParams���ێ�����ShouldShowCompass3D�ϐ����Ď����Atrue�ɂȂ����牝���^�����n�߂܂�
  - ShouldShowCompass3D��false�ɂȂ����ꍇ�͉������܂���

### Sharing�֌W�̎Q�l�ɂȂ肻���ȃR���|�[�l���g

- SharingStage�I�u�W�F�N�g��PrefabSpawnManager�R���|�[�l���g�̃t�B�[���h
- Spawn���������I�u�W�F�N�g�̃N���X���w�肵�܂�