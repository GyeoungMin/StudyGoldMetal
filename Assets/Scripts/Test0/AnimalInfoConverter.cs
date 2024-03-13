using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
public class AnimalInfoConverter : JsonConverter
{
    // �Ű����� objectType�� �θ� Ŭ������ AnimalInfo ���� IsAssignableFrom()�� ���� Ȯ���Ͽ� Convert�� ���������� ���� bool���� return���ش�.
    public override bool CanConvert(Type objectType) => typeof(AnimalInfo).IsAssignableFrom(objectType);

    // ReadJson �޼��带 �����Ͽ� JSON �����͸� ������ȭ�� �� Ư�� ���ǿ� ���� ���ϴ� Ŭ������ ��ȯ�ϵ��� �Ѵ�.
    // �̸� ���Ͽ� JsonConvert.DeserializeObject<T>()�� �ι�° �Ű������� �ش� �޼ҵ带 �����Ͽ� ����� �� �ִ�.
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);

        // type ���� ���� ������ Ŭ������ ��ȯ
        int type = jsonObject["type"].Value<int>();
        if (type == 0)
        {
            return jsonObject.ToObject<HerbivoreInfo>();
        }
        else if (type == 1)
        {
            return jsonObject.ToObject<CarnivoreInfo>();
        }

        return null;
    }

    // WriteJson �޼���� JSON������ ����ȭ �۾��� ��������� �Ϲ������� �� �޼���� �������� �ʰų�, ���ܸ� throw�ϵ��� ���������.
    //public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) { throw new NotImplementedException(); }
    // ����ȭ�� ������ �ʿ䰡 �ִٸ� �����ؾ� �Ѵ�.
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        // value�� AnimalInfo�� ��ü ���� Ȯ���ϰ� �����Ѵ�.
        if (value is AnimalInfo)
        {
            // value == animalInfo �̹Ƿ� as�� ���Ͽ� �������� ������.
            AnimalInfo animalInfo = value as AnimalInfo;

            // ���� ����
            writer.WriteStartObject();

            // animalInfo�� �����ϴ� Properties�� foreach�� �� property�� �ش� name�� value���� �־��־���.
            foreach (var property in animalInfo.GetType().GetProperties())
            {
                writer.WritePropertyName(property.Name);
                writer.WriteValue(property.GetValue(animalInfo));
            }

            // AnimalInfo�� �ڽ� Ŭ������ ���� ������ �ٸ� �Ӽ��� �߰�
            if (animalInfo is HerbivoreInfo herbivoreInfo)
            {
                writer.WritePropertyName("photosyntesis");
                writer.WriteValue(herbivoreInfo.photosyntesis);
            }
            else if (animalInfo is CarnivoreInfo carnivoreInfo)
            {
                writer.WritePropertyName("damage");
                writer.WriteValue(carnivoreInfo.damage);
            }

            // ���� ����
            writer.WriteEndObject();
        }
        else
        {
            // ���� Serialize�� ������ AnimalInfo�� �ƴ� ��� ����ó���� �����Ͽ���.
            throw new InvalidOperationException("Invalid type for serialization: " + value.GetType().Name);
        }
    }
}