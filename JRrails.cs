using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WildBase
{
	class JRrails
	{
		static void Main ( string[] args )
		{
			var mmm = new JRrails();
			mmm.ClsMain( args );
		}
		internal void ClsMain ( string[] args )
		{
			var pref = "東京都";
			var railurl = $"http://express.heartrails.com/api/json";
			var prefecture = args.Length == 0 ? pref : args[0];
			TokyoLineAll( railurl, prefecture );
		}
		internal void TokyoLineAll ( string railurl, string pref )
		{
			var url = $"{railurl}?method=getLines&prefecture={pref}";
			var request = WebRequest.Create(url);
			var response_stream = request.GetResponse().GetResponseStream();
			 if (!response_stream.Equals(null)) 
			 {
				var reader = new StreamReader(response_stream);
				var str = reader.ReadToEnd();
				var obj_from_json = JObject.Parse(str);
				try {
					var search_resultAll = from foo in obj_from_json["response"]["line"]
						select foo;
					foreach(var result in search_resultAll) {
						Console.Write( $"{result} :	" );
						TokyoLine ( railurl, string.Format($"{result}") );
						Console.WriteLine();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine( e.Message );
					Console.WriteLine($"Prefecture {pref} not found.");
				}
			}
		}
		internal void TokyoLine ( string stationsurl, string ln )
		{
			var url = $"{stationsurl}?method=getStations&line={ln}";
			var request = WebRequest.Create(url);
			var response_stream = request.GetResponse().GetResponseStream();
			 if (!response_stream.Equals(null)) 
			 {
				var reader = new StreamReader(response_stream);
				var str = reader.ReadToEnd();
				var obj_from_json = JObject.Parse(str);

				var search_resultAll = from foo in obj_from_json["response"]["station"] 
					select foo;
				var index = 0;
				foreach(var result in search_resultAll) {
					Console.Write( index==0 ? $"{result["name"]}" : $" => {result["name"]}" );
					index++;
				}
			}
		}
	}
}

/*
C:\develop\wrk>csc -platform:x64 -r:Newtonsoft.Json.dll JRrails.cs
Microsoft (R) Visual C# Compiler バージョン 4.2.0-4.22262.19 (46c8f4f5)
Copyright (C) Microsoft Corporation. All rights reserved.

C:develop\wrk>JRrails.exe 千葉県
JR久留里線 :    木更津 => 祇園 => 上総清川 => 東清川 => 横田 => 東横田 => 馬来田 => 下郡 => 小櫃 => 俵田 => 久留里 => 平山 => 上総松丘 => 上総亀山
JR京葉線 :      東京 => 八丁堀 => 越中島 => 潮見 => 新木場 => 葛西臨海公園 => 舞浜 => 新浦安 => 市川塩浜 => 西船橋 => 二俣新町 => 南船橋 => 新習志野 => 海浜幕張 => 検見川浜 => 稲毛海岸 => 千葉みなと => 蘇我
JR内房線 :      千葉 => 本千葉 => 蘇我 => 浜野 => 八幡宿 => 五井 => 姉ケ崎 => 長浦 => 袖ケ浦 => 巌根 => 木更津 => 君津 => 青堀 => 大貫 => 佐貫町 => 上総湊 => 竹岡 => 浜金谷 => 保田 => 安房勝山 => 岩井 => 富浦 => 那古船形 => 館山 => 九重 => 千倉 => 千歳 => 南三原 => 和田浦 => 江見 => 太海 => 安房鴨川
JR外房線 :      千葉 => 本千葉 => 蘇我 => 鎌取 => 誉田 => 土気 => 大網 => 永田 => 本納 => 新茂原 => 茂原 => 八積 => 上総一ノ宮 => 東浪見 => 太東 => 長者町 => 三門 => 大原 => 浪花 => 御宿 => 勝浦 => 鵜原 => 上総興津 => 行川アイランド => 安房小湊 => 安房天津 => 安房鴨川
JR常磐線各駅停車 :      北千住 => 綾瀬 => 亀有 => 金町 => 松戸 => 北松戸 => 馬橋 => 新松戸 => 北小金 => 南柏 => 柏 => 北柏 => 我孫子 => 天王台 => 取手
JR常磐線快速 :  上野 => 日暮里 => 三河島 => 南千住 => 北千住 => 松戸 => 柏 => 我孫子 => 天王台 => 取手
JR成田線 :      佐倉 => 酒々井 => 成田 => 空港第2ビル => 成田空港 => 久住 => 滑河 => 下総神崎 => 大戸 => 佐原 => 香取 => 水郷 => 小見川 => 笹川 => 下総橘 => 下総豊里 => 椎柴 => 松岸
JR成田線我孫子支線 :    成田 => 下総松崎 => 安食 => 小林 => 木下 => 布佐 => 新木 => 湖北 => 東我孫子 => 我孫子
JR東金線 :      大網 => 福俵 => 東金 => 求名 => 成東
JR武蔵野線 :    府中本町 => 北府中 => 西国分寺 => 新小平 => 新秋津 => 東所沢 => 新座 => 北朝霞 => 西浦和 => 武蔵浦和 => 南浦和 => 東浦和 => 東川口 => 南越谷 => 越谷レイクタウン => 吉川 => 吉川美南 => 新三郷 => 三郷 => 南流山 => 新松戸 => 新八柱 => 東松戸 => 市川大野 => 船橋法典 => 西船橋
JR総武本線 :    千葉 => 東千葉 => 都賀 => 四街道 => 物井 => 佐倉 => 南酒々井 => 榎戸 => 八街 => 日向 => 成東 => 松尾 => 横芝 => 飯倉 => 八日市場 => 干潟 => 旭 => 飯岡 => 倉橋 => 猿田 => 松岸 => 銚子
JR総武線 :      三鷹 => 吉祥寺 => 西荻窪 => 荻窪 => 阿佐ケ谷 => 高円寺 => 中野 => 東中野 => 大久保 => 新宿 => 代々木 => 千駄ケ谷 => 信濃町 => 四ツ谷 => 市ヶ谷 => 飯田橋 => 水道橋 => 御茶ノ水 => 秋葉原 => 浅草橋 => 両国 => 錦糸町 => 亀戸 => 平井 => 新小岩 => 小岩 => 市川 => 本八幡 => 下総中山 => 西船橋 => 船橋 => 東船橋 => 津田沼 => 幕張本郷 => 幕張 => 新検見川 => 稲毛 => 西千葉 => 千葉
JR総武線快速 :  東京 => 新日本橋 => 馬喰町 => 錦糸町 => 新小岩 => 市川 => 船橋 => 津田沼 => 稲毛 => 千葉
JR鹿島線 :      香取 => 十二橋 => 潮来 => 延方 => 鹿島神宮 => 鹿島サッカースタジアム
いすみ鉄道 :    大原 => 西大原 => 上総東 => 新田野 => 国吉 => 上総中川 => 城見ヶ丘 => 大多喜 => 小谷松 => 東総元 => 久我原 => 総元 => 西畑 => 上総中野
つくばエクスプレス線 :  秋葉原 => 新御徒町 => 浅草 => 南千住 => 北千住 => 青井 => 六町 => 八潮 => 三郷中央 => 南流山 => 流山セントラルパーク => 流山おおたかの森 => 柏の葉キャンパス => 柏たなか => 守谷 => みらい平 => みどりの => 万博記念公園 => 研究学園 => つくば
ディズニーリゾートライン :      リゾートゲートウェイ・ステーション => 東京ディズニーランド・ステーション => ベイサイド・ステーション => 東京ディズニーシー・ステーション
京成千原線 :    千葉中央 => 千葉寺 => 大森台 => 学園前 => おゆみ野 => ちはら台
京成千葉線 :    京成津田沼 => 京成幕張本郷 => 京成幕張 => 検見川 => 京成稲毛 => みどり台 => 西登戸 => 新千葉 => 京成千葉 => 千葉中央
京成本線 :      京成上野 => 日暮里 => 新三河島 => 町屋 => 千住大橋 => 京成関屋 => 堀切菖蒲園 => お花茶屋 => 青砥 => 京成高砂 => 京成小岩 => 江戸川 => 国府台 => 市川真間 => 菅野 => 京成八幡 => 鬼越 => 京成中山 => 東中山 => 京成西船 => 海神 => 京成船橋 => 大神宮下 => 船橋競馬場 => 谷津 => 京成津田沼 => 京成大久保 => 実籾 => 八千代台 => 京成大和田 => 勝田台 => 志津 => ユーカリが丘 => 京成臼井 => 京成佐倉 => 大佐倉 => 京成酒々井 => 宗吾参道 => 公津の杜 => 京成成田 => 空港第2ビル => 成田空港
京成成田空港線 :        京成高砂 => 東松戸 => 新鎌ヶ谷 => 千葉ニュータウン中央 => 印旛日本医大 => 成田湯川 => 空港第2ビル => 成田空港
北総鉄道 :      京成高砂 => 新柴又 => 矢切 => 北国分 => 秋山 => 東松戸 => 松飛台 => 大町 => 新鎌ヶ谷 => 西白井 => 白井 => 小室 => 千葉ニュータウン中央 => 印西牧の原 => 印旛日本医大
千葉都市モノレール1号線 :       千葉みなと => 市役所前 => 千葉 => 栄町 => 葭川公園 => 県庁前
千葉都市モノレール2号線 :       千葉 => 千葉公園 => 作草部 => 天台 => 穴川 => スポーツセンター => 動物公園 => みつわ台 => 都賀 => 桜木 => 小倉台 => 千城台北 => 千城台
小湊鉄道 :      五井 => 上総村上 => 海士有木 => 上総三又 => 上総山田 => 光風台 => 馬立 => 上総牛久 => 上総川間 => 上総鶴舞 => 上総久保 => 高滝 => 里見 => 飯給 => 月崎 => 上総大久保 => 養老渓谷 => 上総中野
山万ユーカリが丘線 :    ユーカリが丘 => 地区センター => 公園 => 女子大 => 中学校 => 井野
新京成電鉄線 :  松戸 => 上本郷 => 松戸新田 => みのり台 => 八柱 => 常盤平 => 五香 => 元山 => くぬぎ山 => 北初富 => 新鎌ヶ谷 => 初富 => 鎌ヶ谷大仏 => 二和向台 => 三咲 => 滝不動 => 高根公団 => 高根木戸 => 北習志野 => 習志野 => 薬園台 => 前原 => 新津田沼 => 京成津田沼
東京メトロ東西線 :      中野 => 落合 => 高田馬場 => 早稲田 => 神楽坂 => 飯田橋 => 九段下 => 竹橋 => 大手町 => 日本橋 => 茅場町 => 門前仲町 => 木場 => 東陽町 => 南砂町 => 西葛西 => 葛西 => 浦安 => 南行徳 => 行徳 => 妙典 => 原木中山 => 西船橋
東武野田線 :    大宮 => 北大宮 => 大宮公園 => 大和田 => 七里 => 岩槻 => 東岩槻 => 豊春 => 八木崎 => 春日部 => 藤の牛島 => 南桜井 => 川間 => 七光台 => 清水公園 => 愛宕 => 野田市 => 梅郷 => 運河 => 江戸川台 => 初石 => 流山おおたかの森 => 豊四季 => 柏 => 新柏 => 増尾 => 逆井 => 高柳 => 六実 => 新鎌ヶ谷 => 鎌ヶ谷 => 馬込沢 => 塚田 => 新船橋 => 船橋
東葉高速鉄道 :  西船橋 => 東海神 => 飯山満 => 北習志野 => 船橋日大前 => 八千代緑が丘 => 八千代中央 => 村上 => 東葉勝田台
流鉄流山線 :    馬橋 => 幸谷 => 小金城趾 => 鰭ヶ崎 => 平和台 => 流山
芝山鉄道線 :    東成田 => 芝山千代田
都営新宿線 :    新宿 => 新宿三丁目 => 曙橋 => 市ヶ谷 => 九段下 => 神保町 => 小川町 => 岩本町 => 馬喰横山 => 浜町 => 森下 => 菊川 => 住吉 => 西大島 => 大島 => 東大島 => 船堀 => 一之江 => 瑞江 => 篠崎 => 本八幡
銚子電鉄線 :    銚子 => 仲ノ町 => 観音 => 本銚子 => 笠上黒生 => 西海鹿島 => 海鹿島 => 君ヶ浜 => 犬吠 => 外川

C:develop\wrk>JRrails.exe
JR中央線 :      東京 => 神田 => 御茶ノ水 => 水道橋 => 飯田橋 => 市ケ谷 => 四ツ谷 => 信濃町 => 千駄ケ谷 => 代々木 => 新宿 => 大久保 => 東中野 => 中野 => 高円寺 => 阿佐ケ谷 => 荻窪 => 西荻窪 => 吉祥寺 => 三鷹 => 武蔵境 => 東小金井 => 武蔵小金井 => 国分寺 => 西国分寺 => 国立 => 立川 => 日野 => 豊田 => 八王子 => 西八王子 => 高尾
JR中央本線 :    高尾 => 相模湖 => 藤野 => 上野原 => 四方津 => 梁川 => 鳥沢 => 猿橋 => 大月 => 初狩 => 笹子 => 甲斐大和 => 勝沼ぶどう郷 => 塩山 => 東山梨 => 山梨市 => 春日居町 => 石和温泉 => 酒折 => 甲府 => 竜王 => 塩崎 => 韮崎 => 新府 => 穴山 => 日野春 => 長坂 => 小淵沢 => 信濃境 => 富士見 => すずらんの里 => 青柳 => 茅野 => 上諏訪 => 下諏訪 => 岡谷 => みどり湖 => 塩尻 => 洗馬 => 日出塩 => 贄川 => 木曽平沢 => 奈良井 => 藪原 => 宮ノ越 => 原野 => 木曽福島 => 上松 => 倉本 => 須原 => 大桑 => 野尻 => 十二兼 => 南木曽 => 田立 => 坂下 => 落合川 => 中津川 => 美乃坂本 => 恵那 => 武並 => 釜戸 => 瑞浪 => 土岐市 => 多治見 => 古虎渓 => 定光寺 => 高蔵寺 => 神領 => 春日井 => 勝川 => 新守山 => 大曽根 => 千種 => 鶴舞 => 金山 => 名古屋
JR五日市線 :    拝島 => 熊川 => 東秋留 => 秋川 => 武蔵引田 => 武蔵増戸 => 武蔵五日市
JR京浜東北線 :  大宮 => さいたま新都心 => 与野 => 北浦和 => 浦和 => 南浦和 => 蕨 => 西川口 => 川口 => 赤羽 => 東十条 => 王子 => 上中里 => 田端 => 西日暮里 => 日暮里 => 鶯谷 => 上野 => 御徒町 => 秋葉原 => 神田 => 東京 => 有楽町 => 新橋 => 浜松町 => 田町 => 高輪ゲートウェイ => 品川 => 大井町 => 大森 => 蒲田 => 川崎 => 鶴見 => 新子安 => 東神奈川 => 横浜
JR京葉線 :      東京 => 八丁堀 => 越中島 => 潮見 => 新木場 => 葛西臨海公園 => 舞浜 => 新浦安 => 市川塩浜 => 西船橋 => 二俣新町 => 南船橋 => 新習志野 => 海浜幕張 => 検見川浜 => 稲毛海岸 => 千葉みなと => 蘇我
JR八高線 :      八王子 => 北八王子 => 小宮 => 拝島 => 東福生 => 箱根ヶ崎 => 金子 => 東飯能 => 高麗川 => 毛呂 => 越生 => 明覚 => 小川町 => 竹沢 => 折原 => 寄居 => 用土 => 松久 => 児玉 => 丹荘 => 群馬藤岡 => 北藤岡 => 倉賀野 => 高崎
JR南武線 :      川崎 => 尻手 => 矢向 => 鹿島田 => 平間 => 向河原 => 武蔵小杉 => 武蔵中原 => 武蔵新城 => 武蔵溝ノ口 => 津田山 => 久地 => 宿河原 => 登戸 => 中野島 => 稲田堤 => 矢野口 => 稲城長沼 => 南多摩 => 府中本町 => 分倍河原 => 西府 => 谷保 => 矢川 => 西国立 => 立川
JR埼京線 :      大崎 => 恵比寿 => 渋谷 => 新宿 => 池袋 => 板橋 => 十条 => 赤羽 => 北赤羽 => 浮間舟渡 => 戸田公園 => 戸田 => 北戸田 => 武蔵浦和 => 中浦和 => 南与野 => 与野本町 => 北与野 => 大宮
JR宇都宮線 :    上野 => 尾久 => 赤羽 => 浦和 => さいたま新都心 => 大宮 => 土呂 => 東大宮 => 蓮田 => 白岡 => 新白岡 => 久喜 => 東鷲宮 => 栗橋 => 古河 => 野木 => 間々田 => 小山 => 小金井 => 自治医大 => 石橋 => 雀宮 => 宇都宮 => 岡本 => 宝積寺 => 氏家 => 蒲須坂 => 片岡 => 矢板 => 野崎 => 西那須野 => 那須塩原 => 黒磯
JR山手線 :      品川 => 大崎 => 五反田 => 目黒 => 恵比寿 => 渋谷 => 原宿 => 代々木 => 新宿 => 新大久保 => 高田馬場 => 目白 => 池袋 => 大塚 => 巣鴨 => 駒込 => 田端 => 西日暮里 => 日暮里 => 鶯谷 => 上野 => 御徒町 => 秋葉原 => 神田 => 東京 => 有楽町 => 新橋 => 浜松町 => 田町 => 高輪ゲートウェイ
JR常磐線各駅停車 :      北千住 => 綾瀬 => 亀有 => 金町 => 松戸 => 北松戸 => 馬橋 => 新松戸 => 北小金 => 南柏 => 柏 => 北柏 => 我孫子 => 天王台 => 取手
JR常磐線快速 :  上野 => 日暮里 => 三河島 => 南千住 => 北千住 => 松戸 => 柏 => 我孫子 => 天王台 => 取手
JR東海道本線 :  東京 => 新橋 => 品川 => 川崎 => 横浜 => 戸塚 => 大船 => 藤沢 => 辻堂 => 茅ヶ崎 => 平塚 => 大磯 => 二宮 => 国府津 => 鴨宮 => 小田原 => 早川 => 根府川 => 真鶴 => 湯河原 => 熱海 => 函南 => 三島 => 沼津 => 片浜 => 原 => 東田子の浦 => 吉原 => 富士 => 富士川 => 新蒲原 => 蒲原 => 由比 => 興津 => 清水 => 草薙 => 東静岡 => 静岡 => 安倍川 => 用宗 => 焼津 => 西焼津 => 藤枝 => 六合 => 島田 => 金谷 => 菊川 => 掛川 => 愛野 => 袋井 => 御厨 => 磐田 => 豊田町 => 天竜川 => 浜松 => 高塚 => 舞阪 => 弁天島 => 新居町 => 鷲津 => 新所原 => 二川 => 豊橋 => 西小坂井 => 愛知御津 => 三河大塚 => 三河三谷 => 蒲郡 => 三河塩津 => 三ヶ根 => 幸田 => 相見 => 岡崎 => 西岡崎 => 安城 => 三河安城 => 東刈谷 => 野田新町 => 刈谷 => 逢妻 => 大府 => 共和 => 南大高 => 大高 => 笠寺 => 熱田 => 金山 => 尾頭橋 => 名古屋 => 枇杷島 => 清洲 => 稲沢 => 尾張一宮 => 木曽川 => 岐阜 => 西岐阜 => 穂積 => 大垣 => 垂井 => 関ヶ原 => 柏原 => 近江長岡 => 醒ヶ井 => 米原 => 荒尾 => 美濃赤坂
JR横浜線 :      東神奈川 => 大口 => 菊名 => 新横浜 => 小机 => 鴨居 => 中山 => 十日市場 => 長津田 => 成瀬 => 町田 => 古淵 => 淵野辺 => 矢部 => 相模原 => 橋本 => 相原 => 八王子みなみ野 => 片倉 => 八王子
JR横須賀線 :    東京 => 新橋 => 品川 => 西大井 => 武蔵小杉 => 新川崎 => 横浜 => 保土ヶ谷 => 東戸塚 => 戸塚 => 大船 => 北鎌倉 => 鎌倉 => 逗子 => 東逗子 => 田浦 => 横須賀 => 衣笠 => 久里浜
JR武蔵野線 :    府中本町 => 北府中 => 西国分寺 => 新小平 => 新秋津 => 東所沢 => 新座 => 北朝霞 => 西浦和 => 武蔵浦和 => 南浦和 => 東浦和 => 東川口 => 南越谷 => 越谷レイクタウン => 吉川 => 吉川美南 => 新三郷 => 三郷 => 南流山 => 新松戸 => 新八柱 => 東松戸 => 市川大野 => 船橋法典 => 西船橋
JR湘南新宿ライン :      大宮 => 浦和 => 赤羽 => 池袋 => 新宿 => 渋谷 => 恵比寿 => 大崎 => 西大井 => 武蔵小杉 => 新川崎 => 横浜 => 保土ヶ谷 => 東戸塚 => 戸塚 => 大船
JR総武線 :      三鷹 => 吉祥寺 => 西荻窪 => 荻窪 => 阿佐ケ谷 => 高円寺 => 中野 => 東中野 => 大久保 => 新宿 => 代々木 => 千駄ケ谷 => 信濃町 => 四ツ谷 => 市ヶ谷 => 飯田橋 => 水道橋 => 御茶ノ水 => 秋葉原 => 浅草橋 => 両国 => 錦糸町 => 亀戸 => 平井 => 新小岩 => 小岩 => 市川 => 本八幡 => 下総中山 => 西船橋 => 船橋 => 東船橋 => 津田沼 => 幕張本郷 => 幕張 => 新検見川 => 稲毛 => 西千葉 => 千葉
JR総武線快速 :  東京 => 新日本橋 => 馬喰町 => 錦糸町 => 新小岩 => 市川 => 船橋 => 津田沼 => 稲毛 => 千葉
JR青梅線 :      立川 => 西立川 => 東中神 => 中神 => 昭島 => 拝島 => 牛浜 => 福生 => 羽村 => 小作 => 河辺 => 東青梅 => 青梅 => 宮ノ平 => 日向和田 => 石神前 => 二俣尾 => 軍畑 => 沢井 => 御嶽 => 川井 => 古里 => 鳩ノ巣 => 白丸 => 奥多摩
JR高崎線 :      上野 => 尾久 => 赤羽 => 浦和 => さいたま新都心 => 大宮 => 宮原 => 上尾 => 北上尾 => 桶川 => 北本 => 鴻巣 => 北鴻巣 => 吹上 => 行田 => 熊谷 => 籠原 => 深谷 => 岡部 => 本庄 => 神保原 => 新町 => 倉賀野 => 高崎
つくばエクスプレス線 :  秋葉原 => 新御徒町 => 浅草 => 南千住 => 北千住 => 青井 => 六町 => 八潮 => 三郷中央 => 南流山 => 流山セントラルパーク => 流山おおたかの森 => 柏の葉キャンパス => 柏たなか => 守谷 => みらい平 => みどりの => 万博記念公園 => 研究学園 => つくば
上越新幹線 :    東京 => 上野 => 大宮 => 熊谷 => 本庄早稲田 => 高崎 => 上毛高原 => 越後湯沢 => 浦佐 => 長岡 => 燕三条 => 新潟
上野モノレール :        上野動物園東園 => 上野動物園西園
京成押上線 :    押上 => 京成曳舟 => 八広 => 四ツ木 => 京成立石 => 青砥 => 京成高砂
京成本線 :      京成上野 => 日暮里 => 新三河島 => 町屋 => 千住大橋 => 京成関屋 => 堀切菖蒲園 => お花茶屋 => 青砥 => 京成高砂 => 京成小岩 => 江戸川 => 国府台 => 市川真間 => 菅野 => 京成八幡 => 鬼越 => 京成中山 => 東中山 => 京成西船 => 海神 => 京成船橋 => 大神宮下 => 船橋競馬場 => 谷津 => 京成津田沼 => 京成大久保 => 実籾 => 八千代台 => 京成大和田 => 勝田台 => 志津 => ユーカリが丘 => 京成臼井 => 京成佐倉 => 大佐倉 => 京成酒々井 => 宗吾参道 => 公津の杜 => 京成成田 => 空港第2ビル => 成田空港
京成金町線 :    京成高砂 => 柴又 => 京成金町
京成成田空港線 :        京成高砂 => 東松戸 => 新鎌ヶ谷 => 千葉ニュータウン中央 => 印旛日本医大 => 成田湯川 => 空港第2ビル => 成田空港
京浜急行本線 :  泉岳寺 => 品川 => 北品川 => 新馬場 => 青物横丁 => 鮫洲 => 立会川 => 大森海岸 => 平和島 => 大森町 => 梅屋敷 => 京急蒲田 => 雑色 => 六郷土手 => 京急川崎 => 八丁畷 => 鶴見市場 => 京急鶴見 => 花月総持寺 => 生麦 => 京急新子安 => 子安 => 神奈川新町 => 京急東神奈川 => 神奈川 => 横浜 => 戸部 => 日ノ出町 => 黄金町 => 南太田 => 井土ヶ谷 => 弘明寺 => 上大岡 => 屏風浦 => 杉田 => 京急富岡 => 能見台 => 金沢文庫 => 金沢八景 => 追浜 => 京急田浦 => 安針塚 => 逸見 => 汐入 => 横須賀中央 => 県立大学 => 堀ノ内 => 京急大津 => 馬堀海岸 => 浦賀
京浜急行空港線 :        京急蒲田 => 糀谷 => 大鳥居 => 穴守稲荷 => 天空橋 => 羽田空港第3ターミナル => 羽田空港第1・第2ターミナル
京王線 :        新宿 => 笹塚 => 代田橋 => 明大前 => 下高井戸 => 桜上水 => 上北沢 => 八幡山 => 芦花公園 => 千歳烏山 => 仙川 => つつじヶ丘 => 柴崎 => 国領 => 布田 => 調布 => 西調布 => 飛田給 => 武蔵野台 => 多磨霊園 => 東府中 => 府中 => 分倍河原 => 中河原 => 聖蹟桜ヶ丘 => 百草園 => 高幡不動 => 南平 => 平山城址公園 => 長沼 => 北野 => 京王八王子
京王新線 :      新線新宿 => 初台 => 幡ヶ谷 => 笹塚
京王井の頭線 :  渋谷 => 神泉 => 駒場東大前 => 池ノ上 => 下北沢 => 新代田 => 東松原 => 明大前 => 永福町 => 西永福 => 浜田山 => 高井戸 => 富士見ヶ丘 => 久我山 => 三鷹台 => 井の頭公園 => 吉祥寺
京王相模原線 :  調布 => 京王多摩川 => 京王稲田堤 => 京王よみうりランド => 稲城 => 若葉台 => 京王永山 => 京王多摩センター => 京王堀之内 => 南大沢 => 多摩境 => 橋本
京王高尾線 :    北野 => 京王片倉 => 山田 => めじろ台 => 狭間 => 高尾 => 高尾山口
京王動物園線 :  高幡不動 => 多摩動物公園
京王競馬場線 :  東府中 => 府中競馬正門前
北総鉄道 :      京成高砂 => 新柴又 => 矢切 => 北国分 => 秋山 => 東松戸 => 松飛台 => 大町 => 新鎌ヶ谷 => 西白井 => 白井 => 小室 => 千葉ニュータウン中央 => 印西牧の原 => 印旛日本医大
埼玉高速鉄道 :  赤羽岩淵 => 川口元郷 => 南鳩ヶ谷 => 鳩ヶ谷 => 新井宿 => 戸塚安行 => 東川口 => 浦和美園
多摩モノレール :        上北台 => 桜街道 => 玉川上水 => 砂川七番 => 泉体育館 => 立飛 => 高松 => 立川北 => 立川南 => 柴崎体育館 => 甲州街道 => 万願寺 => 高幡不動 => 程久保 => 多摩動物公園 => 中央大学・明星大学 => 大塚・帝京大学 => 松が谷 => 多摩センター
小田急多摩線 :  新百合ヶ丘 => 五月台 => 栗平 => 黒川 => はるひ野 => 小田急永山 => 小田急多摩センター => 唐木田
小田急小田原線 :        新宿 => 南新宿 => 参宮橋 => 代々木八幡 => 代々木上原 => 東北沢 => 下北沢 => 世田谷代田 => 梅ヶ丘 => 豪徳寺 => 経堂 => 千歳船橋 => 祖師ヶ谷大蔵 => 成城学園前 => 喜多見 => 狛江 => 和泉多摩川 => 登戸 => 向ヶ丘遊園 => 生田 => 読売ランド前 => 百合ヶ丘 => 新百合ヶ丘 => 柿生 => 鶴川 => 玉川学園前 => 町田 => 相模大野 => 小田急相模原 => 相武台前 => 座間 => 海老名 => 厚木 => 本厚木 => 愛甲石田 => 伊勢原 => 鶴巻温泉 => 東海大学前 => 秦野 => 渋沢 => 新松田 => 開成 => 栢山 => 富水 => 螢田 => 足柄 => 小田原
御岳登山鉄道 :  滝本 => 御岳山
新交通ゆりかもめ :      新橋 => 汐留 => 竹芝 => 日の出 => 芝浦ふ頭 => お台場海浜公園 => 台場 => 東京国際クルーズターミナル => テレコムセンター => 青海 => 東京ビッグサイト => 有明 => 有明テニスの森 => 市場前 => 新豊洲 => 豊洲
東京りんかい線 :        新木場 => 東雲 => 国際展示場 => 東京テレポート => 天王洲アイル => 品川シーサイド => 大井町 => 大崎
東京メトロ丸ノ内分岐線 :        中野坂上 => 中野新橋 => 中野富士見町 => 方南町
東京メトロ丸ノ内線 :    池袋 => 新大塚 => 茗荷谷 => 後楽園 => 本郷三丁目 => 御茶ノ水 => 淡路町 => 大手町 => 東京 => 銀座 => 霞ケ関 => 国会議事堂前 => 赤坂見附 => 四ツ谷 => 四谷三丁目 => 新宿御苑前 => 新宿三丁目 => 新宿 => 西新宿 => 中野坂上 => 新中野 => 東高円寺 => 新高円寺 => 南阿佐ヶ谷 => 荻窪
東京メトロ千代田線 :    綾瀬 => 北千住 => 町屋 => 西日暮里 => 千駄木 => 根津 => 湯島 => 新御茶ノ水 => 大手町 => 二重橋前 => 日比谷 => 霞ケ関 => 国会議事堂前 => 赤坂 => 乃木坂 => 表参道 => 明治神宮前 => 代々木公園 => 代々木上原 => 北綾瀬
東京メトロ半蔵門線 :    渋谷 => 表参道 => 青山一丁目 => 永田町 => 半蔵門 => 九段下 => 神保町 => 大手町 => 三越前 => 水天宮前 => 清澄白河 => 住吉 => 錦糸町 => 押上
東京メトロ南北線 :      目黒 => 白金台 => 白金高輪 => 麻布十番 => 六本木一丁目 => 溜池山王 => 永田町 => 四ツ谷 => 市ケ谷 => 飯田橋 => 後楽園 => 東大前 => 本駒込 => 駒込 => 西ケ原 => 王子 => 王子神谷 => 志茂 => 赤羽岩淵
東京メトロ日比谷線 :    北千住 => 南千住 => 三ノ輪 => 入谷 => 上野 => 仲御徒町 => 秋葉原 => 小伝馬町 => 人形町 => 茅場町 => 八丁堀 => 築地 => 東銀座 => 銀座 => 日比谷 => 霞ケ関 => 虎ノ門ヒルズ => 神谷町 => 六本木 => 広尾 => 恵比寿 => 中目黒
東京メトロ有楽町線 :    和光市 => 地下鉄成増 => 地下鉄赤塚 => 平和台 => 氷川台 => 小竹向原 => 千川 => 要町 => 池袋 => 東池袋 => 護国寺 => 江戸川橋 => 飯田橋 => 市ヶ谷 => 麹町 => 永田町 => 桜田門 => 有楽町 => 銀座一丁目 => 新富町 => 月島 => 豊洲 => 辰巳 => 新木場
東京メトロ東西線 :      中野 => 落合 => 高田馬場 => 早稲田 => 神楽坂 => 飯田橋 => 九段下 => 竹橋 => 大手町 => 日本橋 => 茅場町 => 門前仲町 => 木場 => 東陽町 => 南砂町 => 西葛西 => 葛西 => 浦安 => 南行徳 => 行徳 => 妙典 => 原木中山 => 西船橋
東京メトロ銀座線 :      渋谷 => 表参道 => 外苑前 => 青山一丁目 => 赤坂見附 => 溜池山王 => 虎ノ門 => 新橋 => 銀座 => 京橋 => 日本橋 => 三越前 => 神田 => 末広町 => 上野広小路 => 上野 => 稲荷町 => 田原町 => 浅草
東京メトロ副都心線 :    和光市 => 地下鉄成増 => 地下鉄赤塚 => 平和台 => 氷川台 => 小竹向原 => 千川 => 要町 => 池袋 => 雑司が谷 => 西早稲田 => 東新宿 => 新宿三丁目 => 北参道 => 明治神宮前〈原宿〉 => 渋谷
東京モノレール羽田線 :  モノレール浜松町 => 天王洲アイル => 大井競馬場前 => 流通センター => 昭和島 => 整備場 => 天空橋 => 羽田空港第3ターミナル => 新整備場 => 羽田空港第1ターミナル => 羽田空港第2ターミナル
東北新幹線 :    東京 => 上野 => 大宮 => 小山 => 宇都宮 => 那須塩原 => 新白河 => 郡山 => 福島 => 白石蔵王 => 仙台 => 古川 => くりこま高原 => 一ノ関 => 水沢江刺 => 北上 => 新花巻 => 盛岡 => いわて沼宮内 => 二戸 => 八戸 => 七戸十和田 => 新青森
東急世田谷線 :  三軒茶屋 => 西太子堂 => 若林 => 松陰神社前 => 世田谷 => 上町 => 宮の坂 => 山下 => 松原 => 下高井戸
東急多摩川線 :  多摩川 => 沼部 => 鵜の木 => 下丸子 => 武蔵新田 => 矢口渡 => 蒲田
東急大井町線 :  大井町 => 下神明 => 戸越公園 => 中延 => 荏原町 => 旗の台 => 北千束 => 大岡山 => 緑が丘 => 自由が丘 => 九品仏 => 尾山台 => 等々力 => 上野毛 => 二子玉川
東急東横線 :    渋谷 => 代官山 => 中目黒 => 祐天寺 => 学芸大学 => 都立大学 => 自由が丘 => 田園調布 => 多摩川 => 新丸子 => 武蔵小杉 => 元住吉 => 日吉 => 綱島 => 大倉山 => 菊名 => 妙蓮寺 => 白楽 => 東白楽 => 反町 => 横浜
東急池上線 :    五反田 => 大崎広小路 => 戸越銀座 => 荏原中延 => 旗の台 => 長原 => 洗足池 => 石川台 => 雪が谷大塚 => 御嶽山 => 久が原 => 千鳥町 => 池上 => 蓮沼 => 蒲田
東急田園都市線 :        渋谷 => 池尻大橋 => 三軒茶屋 => 駒沢大学 => 桜新町 => 用賀 => 二子玉川 => 二子新地 => 高津 => 溝の口 => 梶が谷 => 宮崎台 => 宮前平 => 鷺沼 => たまプラーザ => あざみ野 => 江田 => 市が尾 => 藤が丘 => 青葉台 => 田奈 => 長津田 => つくし野 => すずかけ台 => 南町田グランベリーパーク => つきみ野 => 中央林間
東急目黒線 :    目黒 => 不動前 => 武蔵小山 => 西小山 => 洗足 => 大岡山 => 奥沢 => 田園調布 => 多摩川 => 新丸子 => 武蔵小杉
東武亀戸線 :    亀戸 => 亀戸水神 => 東あずま => 小村井 => 曳舟
東武伊勢崎線 :  浅草 => とうきょうスカイツリー => 押上 => 曳舟 => 東向島 => 鐘ヶ淵 => 堀切 => 牛田 => 北千住 => 小菅 => 五反野 => 梅島 => 西新井 => 竹ノ塚 => 谷塚 => 草加 => 獨協大学前 => 新田 => 蒲生 => 新越谷 => 越谷 => 北越谷 => 大袋 => せんげん台 => 武里 => 一ノ割 => 春日部 => 北春日部 => 姫宮 => 東武動物公園 => 和戸 => 久喜 => 鷲宮 => 花崎 => 加須 => 南羽生 => 羽生 => 川俣 => 茂林寺前 => 館林 => 多々良 => 県 => 福居 => 東武和泉 => 足利市 => 野州山辺 => 韮川 => 太田 => 細谷 => 木崎 => 世良田 => 境町 => 剛志 => 新伊勢崎 => 伊勢崎
東武大師線 :    西新井 => 大師前
東武東上本線 :  池袋 => 北池袋 => 下板橋 => 大山 => 中板橋 => ときわ台 => 上板橋 => 東武練馬 => 下赤塚 => 成増 => 和光市 => 朝霞 => 朝霞台 => 志木 => 柳瀬川 => みずほ台 => 鶴瀬 => ふじみ野 => 上福岡 => 新河岸 => 川越 => 川越市 => 霞ヶ関 => 鶴ヶ島 => 若葉 => 坂戸 => 北坂戸 => 高坂 => 東松山 => 森林公園 => つきのわ => 武蔵嵐山 => 小川町 => 東武竹沢 => みなみ寄居 => 男衾 => 鉢形 => 玉淀 => 寄居
東海道新幹線 :  東京 => 品川 => 新横浜 => 小田原 => 熱海 => 三島 => 新富士 => 静岡 => 掛川 => 浜松 => 豊橋 => 三河安城 => 名古屋 => 岐阜羽島 => 米原 => 京都 => 新大阪
西武国分寺線 :  国分寺 => 恋ヶ窪 => 鷹の台 => 小川 => 東村山
西武多摩川線 :  武蔵境 => 新小金井 => 多磨 => 白糸台 => 競艇場前 => 是政
西武多摩湖線 :  国分寺 => 一橋学園 => 青梅街道 => 萩山 => 八坂 => 武蔵大和 => 多摩湖
西武山口線 :    多摩湖 => 西武園ゆうえんち => 西武球場前
西武拝島線 :    小平 => 萩山 => 小川 => 東大和市 => 玉川上水 => 武蔵砂川 => 西武立川 => 拝島
西武新宿線 :    西武新宿 => 高田馬場 => 下落合 => 中井 => 新井薬師前 => 沼袋 => 野方 => 都立家政 => 鷺ノ宮 => 下井草 => 井荻 => 上井草 => 上石神井 => 武蔵関 => 東伏見 => 西武柳沢 => 田無 => 花小金井 => 小平 => 久米川 => 東村山 => 所沢 => 航空公園 => 新所沢 => 入曽 => 狭山市 => 新狭山 => 南大塚 => 本川越
西武有楽町線 :  練馬 => 新桜台 => 小竹向原
西武池袋線 :    池袋 => 椎名町 => 東長崎 => 江古田 => 桜台 => 練馬 => 中村橋 => 富士見台 => 練馬高野台 => 石神井公園 => 大泉学園 => 保谷 => ひばりヶ丘 => 東久留米 => 清瀬 => 秋津 => 所沢 => 西所沢 => 小手指 => 狭山ヶ丘 => 武蔵藤沢 => 稲荷山公園 => 入間市 => 仏子 => 元加治 => 飯能 => 東飯能 => 高麗 => 武蔵横手 => 東吾野 => 吾野
西武西武園線 :  東村山 => 西武園
西武豊島線 :    練馬 => 豊島園
都営三田線 :    目黒 => 白金台 => 白金高輪 => 三田 => 芝公園 => 御成門 => 内幸町 => 日比谷 => 大手町 => 神保町 => 水道橋 => 春日 => 白山 => 千石 => 巣鴨 => 西巣鴨 => 新板橋 => 板橋区役所前 => 板橋本町 => 本蓮沼 => 志村坂上 => 志村三丁目 => 蓮根 => 西台 => 高島平 => 新高島平 => 西高島平
都営大江戸線 :  新宿西口 => 東新宿 => 若松河田 => 牛込柳町 => 牛込神楽坂 => 飯田橋 => 春日 => 本郷三丁目 => 上野御徒町 => 新御徒町 => 蔵前 => 両国 => 森下 => 清澄白河 => 門前仲町 => 月島 => 勝どき => 築地市場 => 汐留 => 大門 => 赤羽橋 => 麻布十番 => 六本木 => 青山一丁目 => 国立競技場 => 代々木 => 新宿 => 都庁前 => 西新宿五丁目 => 中野坂上 => 東中野 => 中井 => 落合南長崎 => 新江古田 => 練馬 => 豊島園 => 練馬春日町 => 光が丘
都営新宿線 :    新宿 => 新宿三丁目 => 曙橋 => 市ヶ谷 => 九段下 => 神保町 => 小川町 => 岩本町 => 馬喰横山 => 浜町 => 森下 => 菊川 => 住吉 => 西大島 => 大島 => 東大島 => 船堀 => 一之江 => 瑞江 => 篠崎 => 本八幡
都営浅草線 :    西馬込 => 馬込 => 中延 => 戸越 => 五反田 => 高輪台 => 泉岳寺 => 三田 => 大門 => 新橋 => 東銀座 => 宝町 => 日本橋 => 人形町 => 東日本橋 => 浅草橋 => 蔵前 => 浅草 => 本所吾妻橋 => 押上
都電荒川線 :    三ノ輪橋 => 荒川一中前 => 荒川区役所前 => 荒川二丁目 => 荒川七丁目 => 町屋駅前 => 町屋二丁目 => 東尾久三丁目 => 熊野前 => 宮ノ前 => 小台 => 荒川遊園地前 => 荒川車庫前 => 梶原 => 栄町 => 王子駅前 => 飛鳥山 => 滝野川一丁目 => 西ヶ原四丁目 => 新庚申塚 => 庚申塚 => 巣鴨新田 => 大塚駅前 => 向原 => 東池袋四丁目 => 都電雑司ヶ谷 => 鬼子母神前 => 学習院下 => 面影橋 => 早稲田
北陸新幹線 :    金沢 => 新高岡 => 富山 => 黒部宇奈月温泉 => 糸魚川 => 上越妙高 => 飯山 => 長野 => 上田 => 佐久平 => 軽井沢 => 安中榛名 => 高崎 => 本庄早稲田 => 熊谷 => 大宮 => 上野 => 東京
高尾登山電鉄線 :        高尾山 => 清滝
日暮里・舎人ライナー :  日暮里 => 西日暮里 => 赤土小学校前 => 熊野前 => 足立小台 => 扇大橋 => 高野 => 江北 => 西新井大師西 => 谷在家 => 舎人公園 => 舎人 => 見沼代親水公園
JR上野東京ライン :      大宮 => さいたま新都心 => 浦和 => 赤羽 => 尾久 => 日暮里 => 上野 => 東京 => 新橋 => 品川 => 川崎 => 横浜 => 戸塚 => 大船
相鉄・JR直通線 :        新宿 => 渋谷 => 恵比寿 => 大崎 => 西大井 => 武蔵小杉 => 羽沢横浜国大 => 西谷 => 鶴ヶ峰 => 二俣川 => 希望ヶ丘 => 三ツ境 => 瀬谷 => 大和 => 相模大塚 => さがみ野 => かしわ台 => 海老名

C:\develop\wrk>

*/
