# Desc: Generate C# code from proto files
# 递归的搜索当前目录下的所有proto文件，并且生成对应的C#文件
# 生成的C#文件会放在./Assemblies/Protobuf/下
#!/bin/bash
protoRoot=proto_triplematch3d
protocPath=../../../../../../Protobuf/protoc
pbFilePath=../../../../TripleMatch3D/Lua/Net/PbFile
# 使用ls命令遍历文件及文件夹(TopOnly，没有路径)
# 反引号：告诉shell将其中的命令使用命令输出结果代替
for dirName in `ls $protoRoot`
do
	# 如果是文件夹并且存在
	# test -d 判断是否为文件夹并且存在   test -f 是否为文件并且存在
	if test -d $protoRoot/$dirName
	then
		# echo $dirName
		# 遍历文件夹下的protp文件
		for filePath in $protoRoot/$dirName/*.proto
		do
			# echo $filePath
			# ##*{给定字符}(最长匹配)   最后一个{给定字符}及其左边的所有字符都丢弃
			# ##*/ 最后一个/及其左边的字符都丢弃 => 去除路径
			fileName=${filePath##*/}
			# %{给定字符}*  最后一个{给定字符}及其右边的所有字符被丢弃
			# %.* 去除扩展名
			fileNameWithoutExtension=${fileName%.*}

			# echo $fileNameWithoutExtension

			# 判断字符串是否相等。必须要有这些合适的空格
			# shared文件夹下的proto需要引用matchmaker、room、music、exosystem、sheep、triplematch文件夹下的proto，所以设置多个proto_path。因为shared import的时候带了相对路径，所以需要把根目录$protoRoot传进度
			if [ $dirName = shared ];
			then
				$protocPath -o $pbFilePath/$fileNameWithoutExtension.pb.bytes --proto_path=$protoRoot/$dirName --proto_path=$protoRoot --proto_path=$protoRoot/matchmaker --proto_path=$protoRoot/room --proto_path=$protoRoot/music --proto_path=$protoRoot/exosystem --proto_path=$protoRoot/sheep --proto_path=$protoRoot/triplematch $filePath
			else
				$protocPath -o $pbFilePath/$fileNameWithoutExtension.pb.bytes --proto_path=$protoRoot/$dirName $filePath
			fi
		done
	fi
done
